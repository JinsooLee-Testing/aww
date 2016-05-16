<?
    //$ID = $_REQUEST["ID"];
    $conn = mysql_connect("localhost","root","111111") or die("접속에 실패했습니다.");
    mysql_select_db("test_login");
    $query = mysql_query("SELECT SKILLNAME FROM skill_card");
    while($result = mysql_fetch_row($query)){
  	print $result[0];
  	print ":" ;
  }     
  mysql_close($conn);

?>