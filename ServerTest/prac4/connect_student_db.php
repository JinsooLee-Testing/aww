<?php
  $connect=@mysql_connect("localhost","root","111111") or die(mysql_error());
  mysql_select_db("studentdb",$connect) or die(mysql_error());
   echo"db connect complete");
 ?>