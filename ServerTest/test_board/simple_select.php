<?php
  $conn = mysql_connect("localhost","root","111111") or die("접속에 실패했습니다.");
  print "접속 완료<BR>";
  mysql_select_db("board");
  $query = mysql_query("SELECT * FROM board_info ORDER BY MEMBER ");
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
