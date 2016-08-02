<?php
header("Content-Type : text/html; charset=UTF-8");


define('DEF_ACCOUNT'   , 'root');
define('DEF_PASSWORD'  , '111111');
define('DEF_DBNAME'    , 'test_login');
define('DEF_HOST'      , 'localhost');

define('CRYPT_NUM'     , '3');
$GLOBALS['g_isConnected'] = 0;
$mysqli = NULL;

/*
$conn = mysql_connect("localhost", "root", "rk45dh22") or die("im die");
mysql_select_db("dinozoo", $conn);
$sql = "INSERT INTO test (a) values (598)";
mysql_query($sql, $conn);
*/

function connect_db($account = DEF_ACCOUNT, $passwd = DEF_PASSWORD, 
		            $dbname = DEF_DBNAME, $host = DEF_HOST){

	global $GLOBALS;
	global $mysqli;
	
	$mysqli = new mysqli($host, $account, $passwd, $dbname);
	if($mysqli->connect_errno){
		alert("Can not Connected Database");
		return false;
	}
	if(!$GLOBALS['g_isConnected']){
		$GLOBALS['g_isConnected'] = 1;
		$mysqli->set_charset('utf8');
	}
}

function query($str, $account = DEF_ACCOUNT, $passwd = DEF_PASSWORD,
		       $dbname = DEF_DBNAME, $host = DEF_HOST){
	global $GLOBALS;
	global $mysqli;
	
	if(!$GLOBALS['g_isConnected']){
		connect_db($account, $passwd, $dbname, $host);
	}
	$result = $mysqli->query($str);
	return $result;
}

function close_db(){
	global $GLOBALS;
	global $mysqli;
	
	$GLOBALS['g_isConnected'] = 0;
	$mysqli->close();
}

function alert($msg){
	echo "
			<SCRIPT LANGUAGE = 'JAVASCRIPT'>
			alert('$msg');
			</SCRIPT>";
}

function array_to_json( $array ){
	if( !is_array( $array ) ){
		return false;
	}

	$associative = count( array_diff( array_keys($array), array_keys( array_keys( $array )) )); // 배열의 숫자값을 반환해주는 함수 
	if( $associative ){
		$construct = array();
		foreach( $array as $key => $value ){

			// We first copy each key/value pair into a staging array,
			// formatting each key and value properly as we go.

			// Format the key:
			if( is_numeric($key) ){
				$key = "key_$key";
			}
			$key = "\"".addslashes($key)."\"";

			// Format the value:
			if( is_array( $value )){
				//$value = $this->array_to_json( $value );
				$value = array_to_json( $value );
			} else if( !is_numeric( $value ) || is_string( $value ) ){
				$value = "\"".addslashes($value)."\"";
			}

			// Add to staging array:
			$construct[] = "$key: $value";
		}

	// Then we collapse the staging array into the JSON form:
	$result = "{" . implode( ", ", $construct ) . "}";

	} else { // If the array is a vector (not associative):

		$construct = array();
		foreach( $array as $value ){

			// Format the value:
			if( is_array( $value )){
				//$value = $this->array_to_json( $value );
				$value = array_to_json( $value );
			} else if( !is_numeric( $value ) || is_string( $value ) ){
				$value = "\"".addslashes($value)."\"";
			}

			// Add to staging array:
			$construct[] = $value;
		}

		// Then we collapse the staging array into the JSON form:
		$result = "[" . implode( ", ", $construct ) . "]";
	}

	return $result;
}

?>
