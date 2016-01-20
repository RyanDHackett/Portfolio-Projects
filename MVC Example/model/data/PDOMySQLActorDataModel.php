<?php
/* 
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

require_once '../model/data/iActorDataModel.php';
class PDOMySQLActorDataModel implements iActorDataModel
{

    private $dbConnection;
    private $result;
    private $stmt;

    // iActorDataAccess methods
    public function connectToDB()
    {
        try
        {
            $this->dbConnection = new PDO("mysql:host=localhost;dbname=sakila","root","inet2005");
            $this->dbConnection->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
        }
        catch(PDOException $ex)
        {
            die('Could not connect to the Sakila Database via PDO: ' . $ex->getMessage());
        }
    }

    public function closeDB()
    {
        // set a PDO connection object to null to close it
        $this->dbConnection = null;
    }

    public function selectActors($start,$count)
    {

        $selectStatement = "SELECT * FROM actor";
        $selectStatement .= " LIMIT :start,:count;";

        try
        {
            $this->stmt = $this->dbConnection->prepare($selectStatement );
            $this->stmt->bindParam(':start', $start, PDO::PARAM_INT);
            $this->stmt->bindParam(':count', $count, PDO::PARAM_INT);

            $this->stmt->execute();
        }
        catch(PDOException $ex)
        {
            die('Could not select records from Sakila Database via PDO: ' . $ex->getMessage());
        }

    }

    public function searchActors($start,$count,$searchText)
    {
        $q = "SELECT * from actor WHERE last_name like '%$searchText%' OR first_name like '%$searchText%' LIMIT :start, :count";
        try
        {
            $this->stmt = $this->dbConnection->prepare($q);
            $this->stmt->bindParam(':start', $start, PDO::PARAM_INT);
            $this->stmt->bindParam(':count', $count, PDO::PARAM_INT);

            $this->stmt->execute();
        }
        catch(PDOException $ex)
        {
            die('Could not select records from Sakila Database via PDO: ' . $ex->getMessage());
        }
    }
    
    public function selectActorById($actorId)
    {
        $selectStatement = "SELECT * FROM actor";
        $selectStatement .= " WHERE actor_id = :actorID;";

        try
        {
            $this->stmt = $this->dbConnection->prepare($selectStatement);
            $this->stmt->bindParam(':actorID', $actorId, PDO::PARAM_INT);

            $this->stmt->execute();
        }
        catch(PDOException $ex)
        {
            die('Could not select records from Sakila Database via PDO: ' . $ex->getMessage());
        }
    }
    

    public function fetchActors()
    {
        try
        {
            $this->result = $this->stmt->fetch(PDO::FETCH_ASSOC);
            return $this->result;
        }
        catch(PDOException $ex)
        {
            die('Could not retrieve from Sakila Database via PDO: ' . $ex->getMessage());
        }
    }
    
    public function updateActor($actorId,$first_name,$last_name)
    {
        $updateStatement = "UPDATE actor";
        $updateStatement .= " SET first_name = :firstName,last_name=:lastName";
        $updateStatement .= " WHERE actor_id = :actorID;";

        try
        {
            $this->stmt = $this->dbConnection->prepare($updateStatement);
            $this->stmt->bindParam(':firstName', $first_name, PDO::PARAM_STR);
            $this->stmt->bindParam(':lastName', $last_name, PDO::PARAM_STR);
            $this->stmt->bindParam(':actorID', $actorId, PDO::PARAM_INT);

            $this->stmt->execute();

            return $this->stmt->rowCount();
        }
        catch(PDOException $ex)
        {
            die('Could not select records from Sakila Database via PDO: ' . $ex->getMessage());
        }
    }

    public function insertActor($first_name,$last_name)
    {
        try
        {
            $q = "INSERT INTO actor(first_name,last_name) VALUES(:firstName, :lastName)";
            $this->stmt = $this->dbConnection->prepare($q);
            $this->stmt->bindParam(':firstName', $first_name, PDO::PARAM_STR);
            $this->stmt->bindParam(':lastName', $last_name, PDO::PARAM_STR);

            $this->stmt->execute();

            return $this->stmt->rowCount();
        }
        catch(PDOException $ex)
        {
            die('Could not insert record into Sakila Database via PDO: ' . $ex->getMessage());
        }
    }

    public function deleteActor($idToDelete)
    {
        try
        {
            $q = "DELETE FROM film_actor WHERE actor_id=:idToDelete";
            $this->stmt = $this->dbConnection->prepare($q);
            $this->stmt->bindParam(':idToDelete', $idToDelete, PDO::PARAM_INT);
            $this->stmt->execute();
            $this->stmt = $this->dbConnection->prepare("DELETE FROM actor WHERE actor_id=:idToDelete");
            $this->stmt->bindParam(':idToDelete', $idToDelete, PDO::PARAM_INT);
            $this->stmt->execute();
        }
        catch(PDOException $ex)
        {
            die('Could not delete record in Sakila Database via PDO: ' . $ex->getMessage());
        }
    }


    public function fetchActorID($row)
    {
       return $row['actor_id'];
    }

    public function fetchActorFirstName($row)
    {
       return $row['first_name'];
    }

    public function fetchActorLastName($row)
    {
       return $row['last_name'];
    }

}

?>
