<?php
   $ID = $_REQUEST["ID"];
   $PASSWD = $_REQUEST["PASSWD"];
   $conn = mysql_connect("localhost","root","111111") or die("connect_fail.");
  $c1_d = $_POST['ID'];
  $c2_d = $_POST['PASSWD'];
  mysql_select_db("test_login");

  $query = mysql_query("SELECT * FROM login_server WHERE ID LIKE '%$c1_d%'");

  $result = mysql_fetch_array($query);
  if(($_POST['ID'] === $result[0]) && ($_POST['PASSWD'] === $result[1]))
  { 
    print $result[2];
  } 
  else
  {
    echo "connect fail";
  }

  /*else  if(($_POST['ID'] === $result[0]) && ($_POST['PASSWD'] === $result[1]) && ($result[2] === "1"))
  { 
    echo "1";
  } 
    else  if(($_POST['ID'] === $result[0]) && ($_POST['PASSWD'] === $result[1]) && ($result[2] === "2"))
  { 
    echo "2";
  } 
    else  if(($_POST['ID'] === $result[0]) && ($_POST['PASSWD'] === $result[1]) && ($result[2] === "3"))
  { 
    echo "3";
  } 
    else  if(($_POST['ID'] === $result[0]) && ($_POST['PASSWD'] === $result[1]) && ($result[2] === "4"))
  { 
    echo "4";
  } 
    else  if(($_POST['ID'] === $result[0]) && ($_POST['PASSWD'] === $result[1]) && ($result[2] === "5"))
  { 
    echo "5";
  } 
  else
  {
    echo"잘못 보냈음";
  }*/

  
   mysql_close($conn);
?>