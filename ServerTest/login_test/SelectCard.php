<?
    $ID = $_REQUEST["ID"];
    $conn = mysql_connect("localhost","root","111111") or die("접속에 실패했습니다.");
    $c1_d = $_POST['ID'];
    mysql_select_db("test_login");

    //$query = mysql_query("SELECT * FROM login_server WHERE ID LIKE '%$c1_d%'");
    $query = mysql_query("SELECT NAME FROM skill_card WHERE User LIKE  '%$c1_d%'");
    //$result = mysql_fetch_array($query);
    //$result = mysql_fetch_array($query);

    while($result = mysql_fetch_row($query)){
  	print $result[0];
  	print "\n" ;
  	//print $result[1];
  	//print " : ";
  	//print $result[2];
  }

     //$result = mysql_fetch_array($query);
     
?>