<?php
  $conn = mysql_connect("localhost","root","111111") or die("접속에 실패했습니다.");
  print "접속 완료<BR>";
  mysql_select_db("board");

  $h_d = $_POST["h"];

  switch("$h_d");
  {
    case "sel":
    $query = mysql_query("SELECT * FROM board_info ORDER BY MEMBER ");
    break;
    case "ins":
    $a1_d = $_POST["a1"];
    $a2_d = $_POST["a2"];
    mysql_query("INSERT INTO board_info(name,mess) VALUES ('$a1_d','$a2_d') ");
    $query = mysql_query("SELECT * FROM board_info ORDER BY number ");
    break;
    case "del":
     $b1_d = $_POST["b1"];
     mysql_query("DELETE FROM board_info WHERE number = $b1_d");
     $query = mysql_query("SELECT * FROM board_info ORDER BY number ");
    break;
    case "ser":
    $c1_d = $_POST["c1"];  
    $query = mysql_query("SELECT * FROM board_info WHERE mess LIKE '%$c1_d%' ");
    break;
  }
  while($result = mysql_fetch_array($query)){
  	print $result[0];
  	print " : " ;
  	print $result[1];
  	print " : ";
  	print $result[2];
  	print "<BR>";
  }
  mysql_close($conn);
  print "<BR><A HREF = 'simple.html'>메인 화면으로</A>";
?>
