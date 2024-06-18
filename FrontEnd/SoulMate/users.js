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