

var HelpTipDialogMixin = {
    methods: {
        ZatvoriHelpDialog() {
            this.activeHelpTip = false;
        },
        VratiContentPoId(formaId) {
            var helpTip = this.tekst.find(item => item.id === formaId);
            return helpTip.content;
        },
        VratiTitlePoId(formaId) {
            var helpTip = this.tekst.find(item => item.id === formaId);
            return helpTip.title;
        }
    },
    data() {
        return {
            tekst: []
        };
    },
    created() {
        this.tekst = [{
            id: 'korisnik-dodavanje',
            title: 'Dodaj korisnika',
            content: `Važno je odrediti ulogu korisnika ispravno zbog prava pristupa i kasnijih aktivnosti korisnika.`
        },
        {
            id: 'korisnik-list',
            title: 'Korisnici',
            content: `Pregled korisnika sa ponuđenim setom aktivnosti za svakog korisnika.`
        },
        {
            id: 'korisnik-edit',
            title: 'Izmijeni korisnika',
            content: `Unosom obaveznih polja na izmjeni podataka i klikom na dugme "Snimi" mijenjamo podatke korisnika.`
        },
        {
            id: 'izvjestaj-list',
            title: 'Izvještaji',
            content: `Forma za izbor i prikaz izvještaja.
            Nakon izbora izvještaja sa liste klikom na "Prikaži" prikazuje se report forma.
            Zavisno od izvještaja na report formi biramo parametre po kojima pretražujemo izvještaj.`
        }
        ];
    }
};

export default HelpTipDialogMixin;
