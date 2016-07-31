<?   
   $nam = "lee";
   $adds = "dangsoodong";

   $mysqli = new mysqli('localhost','root','111111','just_test');
   if ($mysqli->connect_errno) {
    echo "Failed to connect to MySQL: (" . $mysqli->connect_errno . ") " . $mysqli->connect_error;
}
   if(!$mysqli->query("CALL p(1)")){
    echo "CALL failed: (" . $mysqli->errno . ") " . $mysqli->error;
}
      if(!$mysqli->query("CALL MY_PRO($nam,$adds)")){
    echo "CALL failed: (" . $mysqli->errno . ") " . $mysqli->error;
}
?>