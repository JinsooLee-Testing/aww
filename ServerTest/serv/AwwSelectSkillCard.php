<?php
    $conn = mysql_connect("localhost","root","111111") or die("접속에 실패했습니다.");
    mysql_select_db("test_login") or die("no database");
    $query = mysql_query("SELECT * FROM skill_card WHERE COST =2");
    header ("Content-type: text/xml");
    $xmlBody = "<?xml version='1.0' encoding='utf-8' ?>";
    $xmlBody .= "<XML>";
    
   
     
     while($row = mysql_fetch_array($query))
     {
     	$SKILLNAME = $row["SKILLNAME"];
     	$COST= $row["COST"];
     	$TYPE= $row["TYPE"];
     	$DAMAGE= $row["DAMAGE"];
     	$RANGEVIEW= $row["RANGEVIEW"];
     	$RANGEOFACT= $row["RANGEOFACT"];
     	$xmlBody .='
     	<DATA>
     	    <SKILLNAME>' .$SKILLNAME .'</SKILLNAME>
     	    <COST>' .$COST .'</COST>
     	    <TYPE>' .$TYPE .'</TYPE>
     	    <DAMAGE>' .$DAMAGE .'</DAMAGE>
     	    <RANGEVIEW>' .$RANGEVIEW .'</RANGEVIEW>
     	    <RANGEOFACT>' .$RANGEOFACT .'</RANGEOFACT>
     	</DATA>';
     }
     mysql_close($conn);
     $xmlBody .="</XML>";
     echo $xmlBody;
    //$num_result = mysql_num_rows($query);

    
    /*echo"<?xml version='1.0' encoding='utf-8' ?>";*/
    /*echo"<node>";
     while( $obj = mysql_fetch_object($query)){
      echo "<node> </br>";
      echo "SKILLNAME='$row[SKILLNAME]' </br>";
      echo "COST='$row[COST]' </br>";
      echo "TYPE='$row[TYPE]' </br>";
      echo "DAMAGE='$row[DAMAGE]' </br>";
      echo "RANGEVIEW='$row[RANGEVIEW]' </br>";
      echo "RANGEOFACT='$row[RANGEOFACT]' </br>";
      echo"</node>";
    }*/
    // printf($return); 
    
    

    /* $conn = mysql_connect("localhost","root","111111") or die("접속에 실패했습니다.");
    mysql_select_db("test_login");
    $query = mysql_query("SELECT SKILLNAME FROM skill_card");
    while($result = mysql_fetch_row($query)){
  	print $result[0];
  	print ":" ;
  }     
  mysql_close($conn);*/
?>



  

