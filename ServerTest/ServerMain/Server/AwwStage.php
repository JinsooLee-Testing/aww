<?
$ID = $_REQUEST["ID"];
   $PASSWD = $_REQUEST["PASSWD"];
   $STAGE = $_REQUEST["STAGE"];
    $conn = mysql_connect("localhost","root","111111") or die("connect_fail.");
     mysql_select_db("test_login");
       $c1_d = $_POST['ID'];
       $c2_d = $_POST['PASSWD'];
       $c3_d = $_POST['STAGE'];
     $query=mysql_query("UPDATE login_server SET STAGE = '$c3_d' WHERE ID = '$c1_d' AND PASSWD = '$c2_d'");
     $query = mysql_query("SELECT STAGE FROM login_server WHERE ID LIKE '%$c1_d%'");
     $result = mysql_fetch_array($query);
   switch($result[2])
   {
   	case "0": echo "0"; break;
   	case "1"; echo"1"; break;
   	case "2"; echo"2"; break;
   	case "3"; echo"3"; break;
   	case "4"; echo"4"; break;
   	case "5"; echo"5"; break;
   }
   /*  if(($_POST['ID'] === $result[0]) && ($_POST['PASSWD'] === $result[1]) && ($result[2] === "0"))
  { 
    echo "0";
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
?>