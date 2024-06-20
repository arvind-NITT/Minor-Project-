document.addEventListener('DOMContentLoaded', function() {
    const backgroundGrow = document.getElementById('backgroundGrow');
    const firstImage = 'url(./Images/ban-bg1.jpg)'; // Replace with your first image URL
    const secondImage = 'url(./Images/banner1.jpg)'; // Replace with your second image URL
    let isFirstImage = true;

    backgroundGrow.style.backgroundImage = firstImage;

    backgroundGrow.addEventListener('animationiteration', () => {
      setTimeout(() => {
        backgroundGrow.style.backgroundImage = isFirstImage ? secondImage : firstImage;
        isFirstImage = !isFirstImage;
      }, 0);
    });
  });

  document.addEventListener('DOMContentLoaded', () => {
    const cardSlider = document.querySelector('.card-slider');
    const prevButton = document.querySelector('.prev');
    const nextButton = document.querySelector('.next');
    let currentIndex = 0;
    const totalCards = document.querySelectorAll('.card').length;
    let visibleCards = window.innerWidth <= 768 ? 2 : 4; // Change visibility based on screen size
    const cardWidth = cardSlider.querySelector('.card').offsetWidth;
  
    function updateSliderPosition() {
      const maxIndex = totalCards - visibleCards;
      if (currentIndex > maxIndex) {
        currentIndex = 0; // Reset to first position for circular effect
      } else if (currentIndex < 0) {
        currentIndex = maxIndex; // Set to last position for circular effect
      }
      const offset = -currentIndex * cardWidth;
      cardSlider.style.transform = `translateX(${offset}px)`;
    }
  
    prevButton.addEventListener('click', () => {
      currentIndex -= visibleCards;
      updateSliderPosition();
    });
  
    nextButton.addEventListener('click', () => {
      currentIndex += visibleCards;
      updateSliderPosition();
    });
  
    window.addEventListener('resize', () => {
      visibleCards = window.innerWidth <= 768 ? 2 : 4;
      updateSliderPosition();
    });
  
    // Initialize slider position
    updateSliderPosition();
  });
  