<?
    $conn = mysql_connect("localhost","root","111111") or die("접속에 실패했습니다.");
    mysql_select_db("test_login");
    $query = mysql_query("SELECT NAME FROM obejct_card WHERE PLAYERABLE = 'PLAYER'");
    while($result = mysql_fetch_row($query)){
  	print $result[0];
  	print ":" ;
  } 
  mysql_close($conn);
?>