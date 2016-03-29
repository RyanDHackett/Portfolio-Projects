
<!DOCTYPE html>
<head>
    <link rel="stylesheet" type="text/css" href="assignment1style.css">
</head>

<body>
<div id="banner">
    <p id="bannerText">
        <strong>Actors</strong>
    </p>
</div>

<a class="right" href="<?php echo $_SERVER['PHP_SELF'];?>?action=add">Add</a>
</br>
<form id="searchForm" action="<?php echo $_SERVER['PHP_SELF'];?>" method="POST"></br>
    <label style="padding-left:50px;"><strong>Search Actors</strong></label></br></br>
    <input type="text" id="searchText" name="searchText"><input type="submit"></br></br>
    <input type="hidden" name = "pageNum" value = "<?php echo $pageNum ?>">
    <?php if($pageNum > 1)
    {
        ?>
        <div class="left"><a href=<?php echo $_SERVER['PHP_SELF'];?>?pageNum=<?php echo ($pageNum-1); if(!empty($searchText)){echo "&searchText=".$searchText;}?>>Previous</a></div>
    <?php
    }
    ?>
    <div class="padright"><a href=<?php echo $_SERVER['PHP_SELF'];?>?pageNum=<?php echo ($pageNum+1); if(!empty($searchText)){echo "&searchText=".$searchText;}?>>Next</a></div>
</form>
        <table id="employeeTable">
            <thead>
            <tr>
                <th>Id</th>
                <th>First Name</th>
                <th>Last Name</th>
                <th></th>
                <th></th>
            </tr>
            </thead>
            <?php
            foreach($arrayOfActors as $actor)
            {
                ?>
                <tr>
                    <td><?php echo $actor->getID(); ?></td>
                    <td><?php echo $actor->getFirstName(); ?></td>
                    <td><?php echo $actor->getLastName(); ?></td>
                    <td><a href="<?php echo $_SERVER['PHP_SELF'];?>?action=edit&id=<?php echo $actor->getID(); ?>" class="edit"><img src="../view/images/Pencil.png"> </a></td>
                    <td><a href="<?php echo $_SERVER['PHP_SELF'];?>?action=delete&id=<?php echo $actor->getID(); ?>" class="delete" onclick="return confirmDelete();"><img src="../view/images/Delete.png"> </a></td>
                </tr>
            <?php
            }
            ?>
            <div class="middle"'>Page Number: <?php echo $pageNum?></div>
        </table>



</body>
</html>

<script>
function confirmDelete()
{
    var confirmed = confirm("Are you sure?");
    if (confirmed == true) {
      return true;
    } else {
        return false;
    }
}



</script>