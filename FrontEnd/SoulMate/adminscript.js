
$(document).ready(function () {
    const token = localStorage.getItem('token');
    fetch('http://localhost:5157/api/Admin/GetAllUsers', {
          method: 'GET',
          headers: {
            'Content-Type': 'application/json',
            'Accept': 'application/json',
            'Access-Control-Allow-Origin': "*",
            'Authorization': `Bearer ${token}`
          },

        })
          .then(response => {
            if (!response.ok) {
              // throw new Error('Network response was not ok');
              console.log(response.json());
              alert('Register failed: ' + data);
            }
            return response.json();
          })
          .then(data => {
            console.log('Response:', data);
           
          
          })
          .catch(error => {
            console.error('Error:', error);
            alert('An error occurred: ' + error);
          });


     
    // fetch('http://localhost:5157/api/Admin/GetUserById/119', {
    //       method: 'GET',
    //       headers: {
    //         'Content-Type': 'application/json',
    //         'Accept': 'application/json',
    //         'Access-Control-Allow-Origin': "*",
    //         'Authorization': `Bearer ${token}`
    //       },

    //     })
    //       .then(response => {
    //         if (!response.ok) {
    //           // throw new Error('Network response was not ok');
    //           console.log(response.json());
    //           alert('Register failed: ' + data);
    //         }
    //         return response.json();
    //       })
    //       .then(data => {
    //         console.log('Response:', data);

          
    //       })
    //       .catch(error => {
    //         console.error('Error:', error);
    //         alert('An error occurred: ' + error);
    //       });

    fetch('http://localhost:5157/api/Admin/total-earnings', {
          method: 'GET',
          headers: {
            'Content-Type': 'application/json',
            'Accept': 'application/json',
            'Access-Control-Allow-Origin': "*",
            'Authorization': `Bearer ${token}`
          },

        })
          .then(response => {
            if (!response.ok) {
              // throw new Error('Network response was not ok');
              console.log(response.json());
              alert('Register failed: ' + data);
            }
            return response.json();
          })
          .then(data => {
            console.log('Response:', data);
 var tot= document.getElementById("Total_earning");
 tot.innerHTML=`${data}`
          
          })
          .catch(error => {
            console.error('Error:', error);
            alert('An error occurred: ' + error);
          });


          
    fetch('http://localhost:5157/api/Admin/subscription-counts', {
          method: 'GET',
          headers: {
            'Content-Type': 'application/json',
            'Accept': 'application/json',
            'Access-Control-Allow-Origin': "*",
            'Authorization': `Bearer ${token}`
          },

        })
          .then(response => {
            if (!response.ok) {
              // throw new Error('Network response was not ok');
              console.log(response.json());
              alert('Register failed: ' + data);
            }
            return response.json();
          })
          .then(data => {
            console.log('Response:', data);

          
          })
          .catch(error => {
            console.error('Error:', error);
            alert('An error occurred: ' + error);
          });


          
    fetch('http://localhost:5157/api/Admin/GetAllUsers', {
          method: 'GET',
          headers: {
            'Content-Type': 'application/json',
            'Accept': 'application/json',
            'Access-Control-Allow-Origin': "*",
            'Authorization': `Bearer ${token}`
          },

        })
          .then(response => {
            if (!response.ok) {
              // throw new Error('Network response was not ok');
              console.log(response.json());
              alert('Register failed: ' + data);
            }
            return response.json();
          })
          .then(data => {
            console.log('Response:', data);
          var all_mem = document.getElementById("All_members");
          all_mem.innerHTML=`${data.length}`
          
          })
          .catch(error => {
            console.error('Error:', error);
            alert('An error occurred: ' + error);
          });



    fetch('http://localhost:5157/api/Admin/users-registered-today', {
          method: 'GET',
          headers: {
            'Content-Type': 'application/json',
            'Accept': 'application/json',
            'Access-Control-Allow-Origin': "*",
            'Authorization': `Bearer ${token}`
          },

        })
          .then(response => {
            if (!response.ok) {
              // throw new Error('Network response was not ok');
              console.log(response.json());
              alert('Register failed: ' + data);
            }
            return response.json();
          })
          .then(data => {
            console.log('Response:', data);
               var users_today = document.getElementById("new_users_today");
               users_today.innerHTML=`${data.count}`
          
          })
          .catch(error => {
            console.error('Error:', error);
            alert('An error occurred: ' + error);
          });


     
  
      
  });