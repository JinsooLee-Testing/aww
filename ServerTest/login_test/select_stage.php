<?php
   $STAGE = $_REQUEST["STAGE"];
   $conn = mysql_connect("localhost","root","111111") or die("접속에 실패했습니다.");
  $c1_d = $_POST['STAGE'];
  mysql_select_db("test_login");
  $query = mysql_query("SELECT * FROM login_server WHERE STAGE LIKE '%$c1_d%'");

  $result = mysql_fetch_array($query);
  if($result[2] === "0")
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
  else  if($result[2] === "3"))
  { 
    echo "3";
  } 
  else
  {
    echo"잘못 보냈음";
  }

  
   mysql_close($conn);
?>