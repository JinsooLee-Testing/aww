<?php
   $ID = $_REQUEST["ID"];
   $PASSWD = $_REQUEST["PASSWD"];
$conn = mysql_connect("localhost","root","111111") or die("접속에 실패했습니다.");
  //print "접속 완료<BR>";
  $c1_d = $_POST['ID'];
  $c2_d = $_POST['PASSWD'];
  mysql_select_db("test_login");
  $query = mysql_query("SELECT * FROM login_server WHERE ID, PASSWD LIKE '%$c1_d%','%c2_d' ");
  $result = mysql_fetch_array($query);
  if(($_POST['ID'] === $result[0]) && ($_POST['PASSWD'] === $result[1]))
  { 
  	//print $result[0];
  	//print " : " ;
  	//print $result[1];
    echo"1";
  } 
  else
  {
  	echo"잘못 보냈음";
  }

  //$query = mysql_query("SELECT * FROM test_login WHERE ID LIKE '%$c1_d%', PASSWD LIKE '%$c2_d%' ");
  //while($result = mysql_fetch_array($query)){
  //	print $result[0];
  //	print " : " ;
  //	print $result[1];
 // 	print " : ";
 // 	print $result[2];
 // 	print "<BR>";
 //}
  
   mysql_close($conn);
?>