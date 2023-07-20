<?php
 include_once("Database.php");
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //
    //      ALLUSER CLASS
    //      
    //      CONTAINS ALL QUERIES 
    //
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////
class Alluser{

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // 
    //      ---> ATTRIBUTES
    // 
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////

    private $conn;      // ----> PDO CONNECTION OBJECT
    
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // 
    //      ---> METHOD
    //      CONSTRUCTOR 
    // 
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public function __construct(){
        $db=new Database();
        $this->conn= $db->connect();
    }
    
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // 
    //      ---> METHOD
    //      LOGIN WITH QR-CODE OR PIN + PASSWORD 
    // 
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public function loginAll($GK){
    /////////////////// -----------------------------------------> INICIALISE VARIABLES  
        $_P=hash('sha256',md5($GK));
                $query= '
                SELECT 
                id,
                name1,
                name2
                FROM
                mitarbeiter 
                WHERE 
                SHA2(md5(CONCAT(SHA2(timetouchnr,256),SHA2(pin,256))),256)=:MID
                LIMIT 1
                ';
    /////////////////// -----------------------------------------> PREPARE STATEMENT
                 $stmt = $this->conn->prepare($query);

    /////////////////// -----------------------------------------> BIND DATA
                $stmt->bindParam(':MID', $_P);

    /////////////////// -----------------------------------------> EXECUTE QUERY
                 $stmt->execute();                         
    /////////////////// -----------------------------------------> RETURN RESULT
        return $stmt;
    }
    
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //
    //      ---> METHOD
    //      READ LAST TIMETOUCHES METHOD
    //
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public function getLastTimetouches($_MitarbeiterId){
    /////////////////// -----------------------------------------> INICIALISE VARIABLES 
        $_MId=md5($_MitarbeiterId);
                $query= '
                SELECT 
                datum,
                uhrzeit,
                vorgang
                FROM 
                timetouchbuchungen
                WHERE 
                md5(SHA2(mid,256)) =:MID order by id desc 
                LIMIT 5
                ';
    /////////////////// -----------------------------------------> PREPARE QUERY
                $stmt=$this->conn->prepare($query);
    /////////////////// -----------------------------------------> BIND DATA
                $stmt->bindParam(':MID', $_MId);
    /////////////////// -----------------------------------------> EXECUTE QUERY
                $stmt->execute();                           
    /////////////////// -----------------------------------------> RETURN RESULTS
        return $stmt;
    }
    
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //
    //      ---> METHOD
    //      INSERT NEW TIMETOUCH 
    //
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public function createTimeTouch($_MitarbeiterId,$_PersonalNr,$_Monat,$_Jahr,$_Datum,$_Uhrzeit,$_Buchung,$_ImportDatum,$_User,$_Vorgang,$_ExtDate,$_ExtDateTime) {
    /////////////////// -----------------------------------------> FILTER INPUTS
        $_MitarbeiterId     =   htmlspecialchars(strip_tags($_MitarbeiterId));
        $_PersonalNr        =   htmlspecialchars(strip_tags($_PersonalNr));
        $_Monat             =   htmlspecialchars(strip_tags($_Monat));
        $_Jahr              =   htmlspecialchars(strip_tags($_Jahr));
        $_Datum             =   htmlspecialchars(strip_tags($_Datum));
        $_Uhrzeit           =   htmlspecialchars(strip_tags($_Uhrzeit));
        $_Buchung           =   htmlspecialchars(strip_tags($_Buchung));
        $_ImportDatum       =   htmlspecialchars(strip_tags($_ImportDatum));
        $_User              =   htmlspecialchars(strip_tags($_User));
        $_Vorgang           =   htmlspecialchars(strip_tags($_Vorgang));
        $_ExtDate           =   htmlspecialchars(strip_tags($_ExtDate));
        $_ExtDateTime       =   htmlspecialchars(strip_tags($_ExtDateTime));
    /////////////////// -----------------------------------------> REGEX FILTERING
        if(
            preg_match('/^(@t01ZB)+\d{6,23}+$/',$_Buchung) &&       //@t01ZBddddddddddddddddd
            (preg_match('/^\d{0,4}+$/',$_Jahr)) &&                  //dddd
            (preg_match('/^\d+$/',$_Monat)) &&                      //d
            (preg_match('/^\d{0,4}+$/',$_PersonalNr)) &&            //dddd
            (preg_match('/^\d{0,1}+$/',$_Vorgang)) &&               //d
            (preg_match('/^\d{0,4}+(\-)+\d{0,7}+(\-)+\d{0,10}+\s+\d{0,13}+(:)+\d{0,16}+(:)+\d{0,19}+(\.)+\d{0,23}+$/',$_ExtDate))   //dddd-dd-dd dd:dd:dd.ddd
        ){
    /////////////////// -----------------------------------------> QUERY
            $query = 'INSERT INTO 
            timetouchbuchungen 
            SET 
            mid = :MID, 
            personalnr = :Personalnr, 
            monat = :Monat, 
            jahr = :Jahr, 
            datum = :Datum, 
            uhrzeit = :Uhrzeit, 
            buchung = :Buchung, 
            importDatum = :ImportDatum, 
            user = :User, 
            vorgang = :Vorgang, 
            extDate = :extDate, 
            extDateTime = :extDateTime
            ';
    /////////////////// -----------------------------------------> PREPARE QUERY
            $stmt = $this->conn->prepare($query);                
    /////////////////// -----------------------------------------> BIND DATA
            $stmt->bindParam(':MID', $_MitarbeiterId);
            $stmt->bindParam(':Personalnr', $_PersonalNr);
            $stmt->bindParam(':Monat', $_Monat);
            $stmt->bindParam(':Jahr', $_Jahr);
            $stmt->bindParam(':Datum', $_Datum);
            $stmt->bindParam(':Uhrzeit', $_Uhrzeit);
            $stmt->bindParam(':Buchung', $_Buchung);
            $stmt->bindParam(':ImportDatum', $_ImportDatum);
            $stmt->bindParam(':User', $_User);
            $stmt->bindParam(':Vorgang', $_Vorgang);
            $stmt->bindParam(':extDate', $_ExtDate);
            $stmt->bindParam(':extDateTime', $_ExtDateTime);
    /////////////////// -----------------------------------------> EXECUTE QUERY
            if($stmt->execute()) {
                return true;
            }else{
                return false;
            }    
        }else{
            return false;
        }
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //
    //      ---> METHOD
    //      DESTROY OBJECT ONCE QUERIES ARE DONE   
    //
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public function __destruct() {
    /////////////////// -----------------------------------------> CLOSE CONNECTION OBJECT
        $this->conn=null;
        return true;
    }
}

?>
