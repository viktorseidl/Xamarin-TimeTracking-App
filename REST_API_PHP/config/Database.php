<?php
    
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //
    //      DATABASE CLASS - PDO OBJECT 
    //      
    //      SINGLETON PATTERN
    //
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////
class Database {
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // 
    //      ---> ATTRIBUTES
    // 
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////
    
    private $host = 'localhost';        //HOST ADRESSE
    private $db_name = 'dsschema';      //DATENBANK-NAME 
    private $username = 'root';         //BENUTZERNAME 
    private $password = '';             //PASSWORT
    private $conn;                      //PDO-OBJEKT DER VERBINDUNG

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //
    //      ---> METHOD
    //      CREATE THE PDO INSTANCE
    //
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public function connect(){
    ///////////////// -----------------------------------------> DESTRUCT CONNECTION
        $this->conn=null;
    ///////////////// -----------------------------------------> CREATE CONNECTION
        try{
            $this->conn= new PDO('mysql:host='.$this->host .';dbname='.$this->db_name,$this->username,$this->password);
            $this->conn->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
            
        }catch(PDOException $e){
            echo 'Connection Error:' . $e->getMessage();
        }
    ///////////////// -----------------------------------------> RETURN CONNECTION OBJECT
        return $this->conn;
    }
}
?>