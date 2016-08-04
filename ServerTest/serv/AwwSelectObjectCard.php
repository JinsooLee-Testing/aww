<?PHP
    $conn = mysqli_connect("localhost","root","111111","test_login") or die("접속에 실패했습니다.");
    $query = mysqli_query($conn,"SELECT * FROM object_card where PLAYERABLE = 'NONPLAYER'");
    error_reporting(E_ALL); 
    header ("Content-type: text/xml");
    $xmlBody = "<?xml version='1.0' encoding='utf-8' ?>";
    $xmlBody .= "<XML>";  
    while($row = mysqli_fetch_array($query))
    {
  	    $NAME= $row["NAME"];
     	$ATT= $row["ATT"];
     	$HP= $row["HP"];
     	$MOVE= $row["MOVE"];
     	$VIEW= $row["VIEW"];
     	$SIZE= $row["SIZE"];
     	$ATTRIBUTE= $row["ATTRIBUTE"];
     	$PLAYERABLE= $row["PLAYERABLE"];
     	$xmlBody .='
     	<DATA>
     	    <NAME>' .$NAME .'</NAME>
     	    <ATT>' .$ATT .'</ATT>
     	    <HP>' .$HP .'</HP>
     	    <MOVE>' .$MOVE .'</MOVE>
     	    <VIEW>' .$VIEW .'</VIEW>
     	    <SIZE>' .$SIZE .'</SIZE>
     	    <ATTRIBUTE>' .$ATTRIBUTE .'</ATTRIBUTE>
     	    <PLAYERABLE>' .$PLAYERABLE .'</PLAYERABLE>
     	</DATA>';
    }
       mysqli_close($conn);
     $xmlBody .="</XML>";
     echo $xmlBody;
?>