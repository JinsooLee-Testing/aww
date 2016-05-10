<?
   $ID = $_REQUEST["ID"];
   $PASSWD = $_REQUEST["PASSWD"];
  $STAGE = $_REQUEST["STAGE"];
  $conn = mysql_connect("localhost","root","111111")or die("연결 실패");
  mysql_select_db("test_login");  
  $c1_d = $_POST['ID'];
  $c2_d = $_POST['PASSWD'];
  $c3_d = $_POST['STAGE'];
  //mysql_select_db("test_login");
   $query=mysql_query("UPDATE login_server SET STAGE = '$c3_d' WHERE ID = '$c1_d' AND PASSWD = '$c2_d'");
   $query = mysql_query("SELECT * FROM login_server WHERE ID LIKE '%$c1_d%'");
   $result = mysql_fetch_array($query);
   echo $result[2];
   //printf("%s",$result[0]);
   //echo $result[2];
  /*if($result[2] === "0")
  { 
    echo "0";
  }
  else  if($result[2] === "1")
  { 
    echo "1";
  } 
  else  if($result[2] === "2")
  { 
    echo "2";
  } 
  else  if($result[2] === "3")
  { 
    echo "3";
  } 
  else
  {
    echo"잘못 보냈음";
  }*/

  /*$result = mysql_fetch_array($query);
    while($result = mysql_fetch_array($query)){
  	print $result[0];
  	print " : " ;
  	print $result[1];
  	print " : ";
  	print $result[2];
  	print "<BR>";
  }*/
  mysql_close($conn);
?>  