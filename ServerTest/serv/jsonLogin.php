<?php
   //$ID = "leejinsoo"; //$_REQUEST["ID"];
   //$PASSWD = "1234567";//$_REQUEST["PASSWD"];
   $conn = mysql_connect("localhost","root","111111") or die("접속에 실패했습니다.");
  //$c1_d = $_POST['ID'];
  //$c2_d = $_POST['PASSWD'];
  mysql_select_db("test_login");

 

  $query = mysql_query("SELECT ID,PASSWD,STAGE FROM login_server");
  
  $rows = array();
  while($r = mysql_fetch_assoc($query))
  {
    $rows[] = $r;
  }
  print json_encode($rows);

    //WHERE ID LIKE '%$c1_d%'");

  
  //$result = mysql_fetch_array($query);
  
  /*if(($_POST['ID'] === $result[0]) && ($_POST['PASSWD'] === $result[1]) )
  { 
    echo "connect";
  } 
  else  if(($_POST['ID'] === $result[0]) && ($_POST['PASSWD'] === $result[1]) && ($result[2] === "1"))
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
  else
  {
    echo"잘못 보냈음";
  }*/

  
   mysql_close($conn);
?>