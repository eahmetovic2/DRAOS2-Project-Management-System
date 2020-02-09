import Vue from 'vue';

var apiResource = (resource) => {
    var constructor = (url, params, actions, options) => {
        return Vue.resource(url, params, actions, options);
    };

    return () => {
        return resource(constructor);
    };
};

import Prevod from './prevod';
var PrevodResource = apiResource(Prevod);

import Token from './token';
var TokenResource = apiResource(Token);

import Korisnik from './korisnik';
var KorisnikResource = apiResource(Korisnik);

import Postavke from './postavke';
var PostavkeResource = apiResource(Postavke);

import Sifarnik from './sifarnik';
var SifarnikResource = apiResource(Sifarnik);

import Log from './log';
var LogResource = apiResource(Log);

import Dashboard from './dashboard';
var DashboardResource = apiResource(Dashboard);

import PravoUpravljanjaKorisnikom from './pravoupravljanjakorisnikom';
var PravoUpravljanjaKorisnikomResource = apiResource(PravoUpravljanjaKorisnikom);

import Uloga from './uloga';
var UlogaResource = apiResource(Uloga);

import Projekat from './projekat';
var ProjekatResource = apiResource(Projekat);

import Zahtjev from './zahtjev';
var ZahtjevResource = apiResource(Zahtjev);

import Notifikacije from './notifikacije';
var NotifikacijeResource = apiResource(Notifikacije);



import UlogaTipoviDodatneInformacije from './uloga-tipovi-dodatne-informacije';
var UlogaTipoviDodatneInformacijeResource = apiResource(UlogaTipoviDodatneInformacije);

export {
    TokenResource,
    PrevodResource,
    KorisnikResource,
    PostavkeResource,
    SifarnikResource,
    LogResource,
    DashboardResource,
    PravoUpravljanjaKorisnikomResource,
    UlogaResource,
    ProjekatResource,
    UlogaTipoviDodatneInformacijeResource,
    ZahtjevResource,
    NotifikacijeResource
};
