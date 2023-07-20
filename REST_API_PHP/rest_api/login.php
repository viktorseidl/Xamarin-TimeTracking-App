<?php
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //
    //      LOGIN API --POST (Anmelden am System)
    //
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /*
    <VARIABLES>
      MID           =>   GeneralKey
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
    ////////////////// -----------------------------------------> IF NO DATA DON´T INICIALISE OBJECT
    if($data){
    /////////////////// -----------------------------------------> INICIATE OBJECT
    $Alluser=new Alluser();
    /////////////////// -----------------------------------------> PREPARE ARRAY FOR OUTPUT
    $mit_arr=array();
    $mit_arr['data']=array();
    /////////////////// -----------------------------------------> EXECUTE QUERY
    $result = $Alluser->loginAll($data->MID);
    /////////////////// -----------------------------------------> GET ROWS
    $num= $result ->rowCount();
    /////////////////// -----------------------------------------> IF GREATER 0 THEN
        if($num > 0){        
                    
            while($row=$result->fetch(PDO::FETCH_ASSOC)){
                extract($row);                    
                $mit_item= array(
                    'id' => $id,        
                    'name1' => $name1,         
                    'name2' => $name2
                );
                
                array_push($mit_arr['data'],$mit_item);
            }  
                    
    ///////////////// -----------------------------------------> DESTRUCT OBJECT AND CONNECTION  
            $Alluser->__destruct();        

            echo json_encode($mit_arr);        

        }else{
                
    ///////////////// -----------------------------------------> DESTRUCT OBJECT AND CONNECTION
            $Alluser->__destruct();

            echo json_encode(
                array('message' => 'Passwort oder Benutzer-ID ist falsch!')
            );

        }
    }else{

        echo json_encode(
            array('message' => 'Keine Verbindung zur Datenbank')
        );

    }
?>