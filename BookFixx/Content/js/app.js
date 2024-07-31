document.addEventListener('DOMContentLoaded', function () {

    const basketIcon = document.querySelector('.basket_icon');
    const basketModal = document.querySelector('.basket_modal');
    const closeBtn = document.querySelector('.close-btn');
  
    basketIcon.addEventListener('click', function () {
      basketModal.classList.add('visible');
    });
  
    closeBtn.addEventListener('click', function () {
      basketModal.classList.remove('visible');
    });
  
   
    basketModal.addEventListener('click', function (event) {
      if (event.target === basketModal) {
        basketModal.classList.remove('visible');
      }
    });
  });




