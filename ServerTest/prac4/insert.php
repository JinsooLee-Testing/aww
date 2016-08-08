<meta http-equiv="Contect-Type" contect="text/html; charset=euc-kr"/>
<?php
  $connect = mysql_connect("localhost","root","111111")or die(mysql_error());
  mysql_select_db("test_db",$connect) or die(mysql_error());
  $sql = "insert into player(m_id,m_password)";
  $sql.="values(111111,'111111')";

  $result = mysql_query($sql);
  if($result)
  	echo"sucess record insert";
   else
   	echo"fail record insert";
  mysql_close($connect);
  ?>