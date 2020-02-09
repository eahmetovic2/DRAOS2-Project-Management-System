import moment from 'moment';
import Identity from '../auth/identity';

var CommonMixin = {
    methods: {
        imaPravo(action) {
            return Identity.imaPravo(action);
        },
        frontendModul() {
            return Identity.frontendModul();
        },
        updateDatumRodjenja(maticniBroj, komponenta) {
            if (maticniBroj.length >= 7) {
                const dan = maticniBroj.substring(0, 2);
                const mjesec = maticniBroj.substring(2, 4);
                const dioGodine = maticniBroj.substring(4, 7);
                const godina = parseInt(dioGodine) > 100 ? '1' + dioGodine : '2' + dioGodine;

                var noviDatum = godina + '-' + mjesec + '-' + dan;
                komponenta.update(noviDatum);
            }
        },

        prikaziPolje(stranica, vidljivost) {
            if (stranica == "lista") {
                return vidljivost.indexOf(1) != -1;
            }
            if (stranica == "dodavanje") {
                return vidljivost.indexOf(2) != -1;
            }
            if (stranica == "edit") {
                return vidljivost.indexOf(3) != -1;
            }
            return false;
        },
        getTipPolja(tip) {
            switch (tip) {
                case 1:
                    return 'number';
                case 2:
                    return 'text';
                case 3:
                    return 'select';
                case 4:
                    return 'checkbox';
            }
        },

        mergeQueryToInput(query, inputs) {
            for (let attrname in query) {
                inputs[attrname] = query[attrname];
            }

            // Ovaj dio pretvara atribute objekta u broj ili datum ako moze
            // potrebno je zbog toga sto svi atributi objekta budu stringovi inicijalno
            // a u palikaciji su neki brojevi npr page ili count
            Object.keys(inputs).map(function (key) {
                if (inputs[key] != null && typeof inputs[key] == "string") {
                    let numValue = parseInt(inputs[key]);
                    if (!isNaN(numValue)) {
                        inputs[key] = numValue;
                    } else {
                        var datumValue = moment.utc(inputs[key]);
                        if (datumValue.isValid()) {
                            inputs[key] = datumValue.toDate();
                        }
                    }
                }
            });
            return inputs;
        },

        osvjeziQuery(inputs) {
            inputs = Object.assign({}, inputs);
            for (let attrname in inputs) {
                if (inputs[attrname] == null)
                    inputs[attrname] = "";
            }
            this.$router.push({
                query: inputs
            });
        },
        prikaziDvijeDecimale(broj) {
            if (broj != null && broj != undefined) {
                return broj.toLocaleString(undefined, { minimumFractionDigits: 2 });
            }
            else
                return "0, 00";
        }

    }
};

export default CommonMixin;
