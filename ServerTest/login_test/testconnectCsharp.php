<?php
   $ID = $_REQUEST["ID"];
   $PASSWD = $_REQUEST["PASSWD"];
$conn = mysql_connect("localhost","root","111111") or die("접속에 실패했습니다.");
  //print "접속 완료<BR>";
  mysql_select_db("board");
  $c1_d = $_POST["ID"];
  $c2_d = $_POST["PASSWD"];
  //mysql_query("DELETE FROM board_info WHERE number = $b1_d");
  $query = mysql_query("SELECT * FROM test_login WHERE mess LIKE '%$c1_d%','%$c2_d%' ");
  while($result = mysql_fetch_array($query)){
  	print $result[0];
  	print " : " ;
  	print $result[1];
  	print " : ";
  	print $result[2];
  	print "<BR>";
  }
  mysql_close($conn);

   if(query)
   {
   	echo "정상적인 접속입니다.";
   	//echo $c1_d,$c2_d;
   }
   else
   	echo " 잘못된 접근입니다.";
?>