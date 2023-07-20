<?php
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //
    //      TIMETOUCH API -- POST (Zeiterfassung)
    //
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /*
    <VARIABLES>
      MID           =>   Mitarbeiter-ID
      Personalnr    =>   ChipNummer
      Monat         =>   Monat MM  
      Jahr          =>   Jahr YYYY
      Datum         =>   DD.MM.YYYY 
      Uhrzeit       =>   HH:ii  
      Buchung       =>   2 Kommen - 3 Gehen
      Benutzer      =>   ID
      Vorgang       =>   ID
      ExtDate       =>   YYYY-MM-DD
      ExtDateTime   =>   YYYY-MM-DD HH:ii:ss
    </VARIABLES>
    */
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //
    //      HEADER
    //
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////
    header('Access-Control-Allow-Origin: *');
    header('Content-Type: application/json');
    header('Access-Control-Allow-Methods: POST');
    header('Access-Control-Allow-Headers: Access-Control-Allow-Headers,Content-Type,Access-Control-Allow-Methods, Authorization, X-Requested-With');
    /////////////////// -----------------------------------------> INCLUDES
    include_once('../config/Alluser.php');   
    /////////////////// -----------------------------------------> GET RAW DATA
    $data = json_decode(file_get_contents("php://input"));
    //////////////////  -----------------------------------------> IF NO DATA DON´T INICIALISE OBJECT
    if($data){
    /////////////////// -----------------------------------------> INICIATE OBJECT
    $Alluser=new Alluser();
    /////////////////// -----------------------------------------> CREATE QUERY
    if($Alluser->createTimeTouch( 
        $data->MID,
        $data->Personalnr,
        $data->Monat,
        $data->Jahr,
        $data->Datum,
        $data->Uhrzeit,
        $data->Buchung,
        $data->ImportDatum,
        $data->User,
        $data->Vorgang,
        $data->ExtDate,
        $data->ExtDateTime
        )) {
    
    ///////////////// -----------------------------------------> DESTRUCT OBJECT AND CONNECTION
            $Alluser->__destruct();
        
            echo json_encode(
                array('message' => 'Ihre Zeit wurde erfasst!')
            );

    } else {
        
    ///////////////// -----------------------------------------> DESTRUCT OBJECT AND CONNECTION
        $Alluser->__destruct();

        echo json_encode(
            array('message' => 'Fehler bei der Erfassung aufgetreten!')
        );

    } 
  }else{

        echo json_encode(
            array('message' => 'Keine Verbindung zur Datenbank')
        );
  } 

  
?>