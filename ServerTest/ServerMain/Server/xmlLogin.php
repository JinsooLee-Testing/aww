<?php
 Define( "DATABASE_SERVER", "localhost" ); 
 Define( "DATABASE_USERNAME", "root" ); 
 Define( "DATABASE_PASSWORD", "111111" ); 
 Define( "DATABASE_NAME", "test_login" ); 
  
   
   $mysql = mysql_connect(DATABASE_SERVER, DATABASE_USERNAME, DATABASE_PASSWORD);mysql_select_db( DATABASE_NAME ); 

   echo "<?xml version='1.0' encoding='utf-8'?>"; 
   echo "<node>";

   $result = mysql_query("SELECT * FROM login_server"); 

 while($obj=@mysql_fetch_object($result)) 

{

  $obj->ID=iconv("euc-kr","utf-8",$obj->ID); 

  $obj->PASSWD=iconv("euc-kr","utf-8",$obj->PASSWD); 
  $obj->STAGE=iconv("euc-kr","utf-8",$obj->STAGE);

  $return .= "<node ID='$obj->ID' PASSWD='$obj->PASSWD' STAGE='$obj->STAGE'/>";


    }

 printf($return); 

 echo "</node>"; 
?>