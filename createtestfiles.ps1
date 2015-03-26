$i=1
while ($i -le 50000) {
$filename = "h:\test\Test1\" + $i + "test.txt"
"test" | out-file $filename
$filename = "h:\test\Test1\Sub\" + $i + "test.txt"
"test" | out-file $filename
$filename = "h:\test\Test2\" + $i + "test.txt"
"test" | out-file $filename
$i++
}
