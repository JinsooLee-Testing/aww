<?php
 Define( "DATABASE_SERVER", "localhost" ); 
 Define( "DATABASE_USERNAME", "root" ); 
 Define( "DATABASE_PASSWORD", "111111" ); 
 Define( "DATABASE_NAME", "test_login" ); 
   $ID = $_REQUEST["ID"];
   $PASSWD = $_REQUEST["PASSWD"];
   $c1_d = $_POST['ID'];
   $c2_d = $_POST['PASSWD'];
   $conn = mysql_connect(DATABASE_SERVER, DATABASE_USERNAME, DATABASE_PASSWORD);mysql_select_db( DATABASE_NAME ); 
    
   $query = "SELECT * FROM login_server  WHERE ID LIKE '%$c1_d%'  "; 
   
   
   $result = mysql_query($query); 
   
    if(!$result)
      id("DB access failed : " . mysql_error());

    $num_result = mysql_num_rows($result);

    header ("Contect-type: text/xml");
    echo"<xml version=\"1.0\" encoding=\"utf-8\" ?> \n";

    echo "<record>";
    for($i=0; $i<$num_result; ++$i)
    {
      $row = mysql_fetch_array($result);

      echo "<item> ";
      echo "ID='$row[ID]'";
      echo "PASSWD='$row[PASSWD]'";
      echo "STAGE='$row[STAGE]'";
      echo $i."</item>";
    }
    echo"</record>";

 /*while($obj=@mysql_fetch_object($result)) 

{

  $obj->ID=iconv("euc-kr","utf-8",$obj->ID); 

  $obj->PASSWD=iconv("euc-kr","utf-8",$obj->PASSWD); 
  $obj->STAGE=iconv("euc-kr","utf-8",$obj->STAGE);

  $return .= "<node ID='$obj->ID' PASSWD='$obj->PASSWD' STAGE='$obj->STAGE'/>";


    }

 printf($return); */

   mysql_close($conn);
?>