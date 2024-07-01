
$(document).ready(function () {
    const token = localStorage.getItem('token');
    fetch('http://localhost:5157/api/Subscription/GetSubscriptionByUserId', {
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
        const matchContainer = document.getElementById('standard_plan');
        const endDate = new Date(data.endDate).toLocaleDateString();
        matchContainer.innerHTML = ` <div class="boxcontent" >Standard plan

              </div>
              <div class="iii"><img src="./images/91.png" alt="" srcset="">
              </div>
              <div class="plans">
                <div class="plan1">Plan name: <strong>${data.type}</strong> </div>
                <div class="plan2">Validity:<strong> 2 Months</strong>
                </div>
                <div class="plan3">Valid till :<strong>${endDate}</strong>
                </div>

              </div>
              <botton class="btn1">
                <div class="UPGRADENOW
          ">UPGRADE</div>
              </botton>
`;
    
      })
      .catch(error => {
        console.error('Error:', error);
        alert('An error occurred: ' + error);
      });


     
  
      
  });

  