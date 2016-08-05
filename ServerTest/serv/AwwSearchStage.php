<?
   $ID = $_REQUEST["ID"];
   $PASSWD = $_REQUEST["PASSWD"];
   $STAGE = $_REQUEST["STAGE"];
    $conn = mysql_connect("localhost","root","111111") or die("connect_fail.");
     mysql_select_db("test_login");
       $c1_d = $_POST['ID'];
       $c2_d = $_POST['PASSWD'];
       $c3_d = $_POST['STAGE'];
     $query = mysql_query("SELECT STAGE FROM login_server WHERE ID LIKE '%$c1_d%'");
     $result = mysql_fetch_array($query);
   switch($result[0])
   {
   	case "0": echo "0"; break;
   	case "1"; echo"1"; break;
   	case "2"; echo"2"; break;
   	case "3"; echo"3"; break;
   	case "4"; echo"4"; break;
   	case "5"; echo"5"; break;
   }
   mysql_close($conn);
?>