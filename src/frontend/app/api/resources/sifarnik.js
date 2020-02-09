export default resource => {
  return resource('sifarnik/', null, {
    nekesirano: {
      method: 'GET',
      url: 'sifarnik/{sifarnik}/nekesirano'
    },
    polja: {
      method: 'GET',
      url: 'sifarnik/{tipSifarnika}/polja'
    }
  });
};
