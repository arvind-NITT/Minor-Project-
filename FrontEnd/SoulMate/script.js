document.querySelectorAll('.mycontainer3').forEach(item => {
    item.addEventListener('click', () => {
      // Remove 'selected' class from all .mycontainer3 elements
      document.querySelectorAll('.mycontainer3').forEach(el => el.classList.remove('selected'));
      // Add 'selected' class to the clicked element
      item.classList.add('selected');
    });
  });