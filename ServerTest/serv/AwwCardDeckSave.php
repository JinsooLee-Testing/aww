<?
   
   error_reporting(E_ALL);
   $RecvCard = $_REQUEST["CARD"];
   $decode = json_decode($RecvCard,true);
   //print_r($decode);

     $conn = mysqli_connect("localhost","root","111111","test_login") or die("접속에 실패했습니다.");

   foreach($decode as $item)
   {
   	foreach($item as $key)
   	{
   		 
      $query = mysqli_query($conn,"UPDATE user_deck_list set Cardname = '$key'");
      echo $key." ";
   	}
   }

   $query = mysqli_query($conn,"SELECT * FROM user_deck_list ");
      while($row = mysqli_fetch_array($query))
    {
    	echo $row[0];
    }
   //var_dump(json_decode($RecvCard,true));

   /*foreach($decode as $key=>$value)
   {
   	echo $value."<br>";
   }*/
   //$list = $decode->first;
   //var_dump($decode);

   /*foreach($decode as $x)
   	{
   		echo $x;
   		echo "</br>";
   		} */
   //$first = $decode->[0];
   //echo implode(',',$decode);
   //echo implode(",",$decode); 
   //echo $RecvCard;

   
  
   /*header ("Content-type: text/xml");
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
     echo $xmlBody;*/

?>