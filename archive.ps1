################################################################
### DATE     : 28-FEB-2014                                   ###
### DESC     : This script can archive large folders with a  ###
###            number of parameters.                         ### 
### REV      : 28-FEB-2014 - JBOR - Created                  ###
################################################################

# Do not change this file to add folders, use archive.xml for configuration

# Functions first
Function zipper() 
{
	# This function is called to zip the file list
	$count=0
	
	# Determine the archive file name
	$archive = $destdir + "\" + $interfacename + "_" + $format + ".zip"
		
	# Open and test the archive
	$error.clear()
	try { $archiver=[System.IO.Compression.ZipFile]::Open($archive, "Update") }
	catch { "Exception" }
	if ($error) { break }
		
	foreach ($file in filelist)
	{
		# As a destination we will only strip the Procesdir, as we want subdirectories added in the zip
		$destination=($file.FullName).Substring((($parameter.ProcesDir).length) + 1)
			
		# Determine the source
		$source = ($file.FullName)
		
		# The actual copy-to-zip action
		$null = [System.IO.Compression.ZipFileExtensions]::CreateEntryFromFile($archiver,$source,$destination,$compressionlevel)
	
		# Remove the file that was archived
		Remove-Item $source -Force -Recurse -ErrorAction SilentlyContinue		
				
		# Add one to the counter
		$count++
	}		
	# Now that everything has been zipped the archive file will be closed and the counter is returned
	$archiver.Dispose()
	return $count
}

Function remover()
{
	# This function is called to clean-up the folder, it processes each file individually
	$count=0
	
	foreach ($file in filelist) 
	{
		# Remove the file
		Remove-Item $file.fullname -Force -Recurse -ErrorAction SilentlyContinue

		# Add one to the counter
		$count++
	}

	# The counter is returned
	return $count
}

Function filelist()
{
	# Retrieve the file list (to be improved)
	return Get-ChildItem $procesdir -Attributes !R+!S -Recurse:$recurse -Exclude $exclude -include $include| where {!$_.PsisContainer -and $_.lastwritetime -lt (get-date).adddays(-$parameter.Retention) -and (Get-Date($_.lastwritetime) -format yyyyMMdd) -lt ($lastdate) -and (Get-Date($_.lastwritetime) -format yyyyMMdd) -ge ($firstdate)}|where {$recurse -eq $False -and $_.DirectoryName -eq ($parameter.ProcesDir) -or $recurse -eq $True}| Sort-Object lastwritetime -descending
}
	
# The script starts here

# To calculate statistics it creates some variables
$counter = 0
$rmcounter = 0
$starttime = Get-Date

# Import XML file with the configuration
[xml]$def = Get-Content archive.xml

# Load Compression
[Reflection.Assembly]::LoadWithPartialName( "System.IO.Compression.FileSystem" )|Out-Null
$compressionLevel = [System.IO.Compression.CompressionLevel]::Optimal

# Loop through each parameter from the XML file
foreach( $parameter in $def.ParametersDataSet.Parameter) 
{ 
	# The ProcesDir parameter is mandatory
	if (!$parameter.ProcesDir) { break } else { $procesdir = $parameter.ProcesDir+"\*" }
	
	# If no archive-dir has been provided use the process-dir itself, in case it does not exist, create it
	if ($parameter.ArchiveDir) { $destdir = $parameter.ArchiveDir } else { $destdir = $parameter.ProcesDir }
	if (( Test-Path -path $destdir) -ne $True) { New-Item $destdir -ItemType directory|out-null }
		
	# Pick a file that has to be archived and look for more files within the same time stamp (that need to go to the same archive). In case something is not right here, the script might result in a loop...
    	$interfacename = ($parameter.InterfaceName)	
    	[boolean]$recurse = if($parameter.Recursive -eq "true") { $true } else { $false }

	# Move all excludes into an array, and eventually put the archive name into it as well
	[array]$exclude = if($parameter.Exclude) { $parameter.Exclude } else { "''" }
	if ($interfacename) { $exclude += $interfacename+"_*.zip" }

	# Move all includes into an array
	[array]$include = if($parameter.Include) { $parameter.Include } else { "" }

	# For performance it's better to pick a file, and then determine whether there are other files with similar date attributes, to move them into the zip all at once
	$lastitem = ""

	$firstdate = 0
	$lastdate = [int](get-date -format yyyyMMdd)+1

	while ($item = filelist | select -last 1)
	{
		if ($item.fullname -eq $lastitem) { break } else { $lastitem = $item.fullname }
		
		# Determine the date formatting (provided by the timespan parameter) for the zip's file name and start and ending date for the timespan 
		switch ($parameter.Timespan) 
		{ 
			D 
			{	 
				$format = Get-date($item.LastWriteTime) -format yyyyMMdd
				$lastdate = (Get-date($item.LastWriteTime.adddays(1)) -format yyyyMMdd)
				$firstdate = (Get-date($item.LastWriteTime) -format yyyyMMdd)
			}
			W 
			{ 
				# This is the .NET non ISO proof week number...
				$format = Get-date($item.LastWriteTime) -uformat %Y-W%V
				$day = Get-date($item.LastWriteTime) -Uformat %u
				$lastdate = ($item.LastWriteTime.adddays((0-($day))+7).tostring("yyyyMMdd")) 
				$firstdate = ($item.LastWriteTime.adddays(0-($day)).tostring("yyyyMMdd"))
			} 
			M 
			{
				$format = Get-date($item.LastWriteTime) -format yyyyMM			
				$lastdate = (Get-date($item.LastWriteTime.addmonths(1)) -format yyyyMM01)
				$firstdate = (Get-date($item.LastWriteTime) -format yyyyMM01)
			}
			Y
			{				
				$format = Get-date($item.LastWriteTime) -format yyyy
				$lastdate = (Get-date($item.LastWriteTime.addyears(1)) -format yyyy0101)
				$firstdate = (Get-date($item.LastWriteTime) -format yyyy0101)
			}
			R {}
			default { Write-Host "Warning, no valid timespan has been provided for ($parameter.ProcesDir)";Exit}
		}
		# Ok let's go
		if ($parameter.Timespan -eq "R") { $rmcounter += remover } else { $counter += zipper }
	}
}	

# Finishing by providing statistics

#$logfile="archive_"+(Get-date -format yyyyMMddhhmmss)+".log"

write-host "Finished archiving a total of $counter files and removed a total of $rmcounter files. Time statistics:"
New-TimeSpan $starttime (Get-Date) | Format-List Minutes, Seconds 

	
