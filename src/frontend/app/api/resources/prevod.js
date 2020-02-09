export default resource => {
    return resource('prevod/{tabela}/id/{id}', null, {
     
      getPrevodLista: {
        method: 'GET',
        url: 'prevod/{tabela}/id/{id}'
      }
      ,
      prevedi: {
        method: 'POST',
        url: 'prevod/{tabela}/id/{id}'
      }
    });
  };
  