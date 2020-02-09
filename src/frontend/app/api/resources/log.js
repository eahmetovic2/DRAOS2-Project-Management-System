export default resource => {
    return resource('log/{logId}', null, {
        level: {
            method: 'GET',
            url: 'log/log_level'
        },
        kategorija: {
            method: 'GET',
            url: 'log/log_kategorija'
        },
        akcije: {
            method: 'GET',
            url: 'log/log_akcija'
        },
        entiteti: {
            method: 'GET',
            url: 'log/log_entiteti'
        },
        akcija: {
            method: 'GET',
            url: 'log/akcija'
        },
        entitet: {
            method: 'GET',
            url: 'log/entitet'
        },
        entitetById: {
            method: 'GET',
            url: 'log/entitet/{entitetId}/{entitet}'
        },
        entitet_verzija: {
            method: 'GET',
            url: 'log/entitet/{entitetId}/{entitet}/verzija/{verzijaId}'
        }
    });
};