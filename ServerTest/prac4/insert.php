<meta http-equiv="Contect-Type" contect="text/html; charset=euc-kr"/>
<?php
  $connect = mysql_connect("localhost","root","111111")or die(mysql_error());
  mysql_select_db("test_db",$connect) or die(mysql_error());
  echo"sucess db";

  mysql_close($connect);
  ?>