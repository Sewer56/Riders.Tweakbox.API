using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Riders.Tweakbox.API.Application.Commands.v1.User
{
    /// <summary>
    /// List of countries by their ISO 3166-1 alpha-3 name. 
    /// </summary>
    public enum CountryDto
    {
        [Country(Name = "Andorra", Continent = Continent.Europe, TwoLetterCode ="AD")]
        AND = 0,

        [Country(Name = "United Arab Emirates", Continent = Continent.Asia, TwoLetterCode ="AE")]
        ARE = 1,

        [Country(Name = "Afghanistan", Continent = Continent.Asia, TwoLetterCode ="AF")]
        AFG = 2,

        [Country(Name = "Antigua and Barbuda", Continent = Continent.NorthAmerica, TwoLetterCode ="AG")]
        ATG = 3,

        [Country(Name = "Anguilla", Continent = Continent.NorthAmerica, TwoLetterCode ="AI")]
        AIA = 4,

        [Country(Name = "Albania", Continent = Continent.Europe, TwoLetterCode ="AL")]
        ALB = 5,

        [Country(Name = "Armenia", Continent = Continent.Asia, TwoLetterCode ="AM")]
        ARM = 6,

        [Country(Name = "Angola", Continent = Continent.Africa, TwoLetterCode ="AO")]
        AGO = 7,

        [Country(Name = "Antarctica", Continent = Continent.Antarctica, TwoLetterCode ="AQ")]
        ATA = 8,

        [Country(Name = "Argentina", Continent = Continent.SouthAmerica, TwoLetterCode ="AR")]
        ARG = 9,

        [Country(Name = "American Samoa", Continent = Continent.Oceania, TwoLetterCode ="AS")]
        ASM = 10,

        [Country(Name = "Austria", Continent = Continent.Europe, TwoLetterCode ="AT")]
        AUT = 11,

        [Country(Name = "Australia", Continent = Continent.Oceania, TwoLetterCode ="AU")]
        AUS = 12,

        [Country(Name = "Aruba", Continent = Continent.NorthAmerica, TwoLetterCode ="AW")]
        ABW = 13,

        [Country(Name = "Aland Islands", Continent = Continent.Europe, TwoLetterCode ="AX")]
        ALA = 14,

        [Country(Name = "Azerbaijan", Continent = Continent.Asia, TwoLetterCode ="AZ")]
        AZE = 15,

        [Country(Name = "Bosnia and Herzegovina", Continent = Continent.Europe, TwoLetterCode ="BA")]
        BIH = 16,

        [Country(Name = "Barbados", Continent = Continent.NorthAmerica, TwoLetterCode ="BB")]
        BRB = 17,

        [Country(Name = "Bangladesh", Continent = Continent.Asia, TwoLetterCode ="BD")]
        BGD = 18,

        [Country(Name = "Belgium", Continent = Continent.Europe, TwoLetterCode ="BE")]
        BEL = 19,

        [Country(Name = "Burkina Faso", Continent = Continent.Africa, TwoLetterCode ="BF")]
        BFA = 20,

        [Country(Name = "Bulgaria", Continent = Continent.Europe, TwoLetterCode ="BG")]
        BGR = 21,

        [Country(Name = "Bahrain", Continent = Continent.Asia, TwoLetterCode ="BH")]
        BHR = 22,

        [Country(Name = "Burundi", Continent = Continent.Africa, TwoLetterCode ="BI")]
        BDI = 23,

        [Country(Name = "Benin", Continent = Continent.Africa, TwoLetterCode ="BJ")]
        BEN = 24,

        [Country(Name = "Saint Barthelemy", Continent = Continent.NorthAmerica, TwoLetterCode ="BL")]
        BLM = 25,

        [Country(Name = "Bermuda", Continent = Continent.NorthAmerica, TwoLetterCode ="BM")]
        BMU = 26,

        [Country(Name = "Brunei", Continent = Continent.Asia, TwoLetterCode ="BN")]
        BRN = 27,

        [Country(Name = "Bolivia", Continent = Continent.SouthAmerica, TwoLetterCode ="BO")]
        BOL = 28,

        [Country(Name = "Bonaire, Saint Eustatius and Saba ", Continent = Continent.NorthAmerica, TwoLetterCode ="BQ")]
        BES = 29,

        [Country(Name = "Brazil", Continent = Continent.SouthAmerica, TwoLetterCode ="BR")]
        BRA = 30,

        [Country(Name = "Bahamas", Continent = Continent.NorthAmerica, TwoLetterCode ="BS")]
        BHS = 31,

        [Country(Name = "Bhutan", Continent = Continent.Asia, TwoLetterCode ="BT")]
        BTN = 32,

        [Country(Name = "Bouvet Island", Continent = Continent.Antarctica, TwoLetterCode ="BV")]
        BVT = 33,

        [Country(Name = "Botswana", Continent = Continent.Africa, TwoLetterCode ="BW")]
        BWA = 34,

        [Country(Name = "Belarus", Continent = Continent.Europe, TwoLetterCode ="BY")]
        BLR = 35,

        [Country(Name = "Belize", Continent = Continent.NorthAmerica, TwoLetterCode ="BZ")]
        BLZ = 36,

        [Country(Name = "Canada", Continent = Continent.NorthAmerica, TwoLetterCode ="CA")]
        CAN = 37,

        [Country(Name = "Cocos Islands", Continent = Continent.Asia, TwoLetterCode ="CC")]
        CCK = 38,

        [Country(Name = "Democratic Republic of the Congo", Continent = Continent.Africa, TwoLetterCode ="CD")]
        COD = 39,

        [Country(Name = "Central African Republic", Continent = Continent.Africa, TwoLetterCode ="CF")]
        CAF = 40,

        [Country(Name = "Republic of the Congo", Continent = Continent.Africa, TwoLetterCode ="CG")]
        COG = 41,

        [Country(Name = "Switzerland", Continent = Continent.Europe, TwoLetterCode ="CH")]
        CHE = 42,

        [Country(Name = "Ivory Coast", Continent = Continent.Africa, TwoLetterCode ="CI")]
        CIV = 43,

        [Country(Name = "Cook Islands", Continent = Continent.Oceania, TwoLetterCode ="CK")]
        COK = 44,

        [Country(Name = "Chile", Continent = Continent.SouthAmerica, TwoLetterCode ="CL")]
        CHL = 45,

        [Country(Name = "Cameroon", Continent = Continent.Africa, TwoLetterCode ="CM")]
        CMR = 46,

        [Country(Name = "China", Continent = Continent.Asia, TwoLetterCode ="CN")]
        CHN = 47,

        [Country(Name = "Colombia", Continent = Continent.SouthAmerica, TwoLetterCode ="CO")]
        COL = 48,

        [Country(Name = "Costa Rica", Continent = Continent.NorthAmerica, TwoLetterCode ="CR")]
        CRI = 49,

        [Country(Name = "Cuba", Continent = Continent.NorthAmerica, TwoLetterCode ="CU")]
        CUB = 50,

        [Country(Name = "Cabo Verde", Continent = Continent.Africa, TwoLetterCode ="CV")]
        CPV = 51,

        [Country(Name = "Curacao", Continent = Continent.NorthAmerica, TwoLetterCode ="CW")]
        CUW = 52,

        [Country(Name = "Christmas Island", Continent = Continent.Oceania, TwoLetterCode ="CX")]
        CXR = 53,

        [Country(Name = "Cyprus", Continent = Continent.Europe, TwoLetterCode ="CY")]
        CYP = 54,

        [Country(Name = "Czechia", Continent = Continent.Europe, TwoLetterCode ="CZ")]
        CZE = 55,

        [Country(Name = "Germany", Continent = Continent.Europe, TwoLetterCode ="DE")]
        DEU = 56,

        [Country(Name = "Djibouti", Continent = Continent.Africa, TwoLetterCode ="DJ")]
        DJI = 57,

        [Country(Name = "Denmark", Continent = Continent.Europe, TwoLetterCode ="DK")]
        DNK = 58,

        [Country(Name = "Dominica", Continent = Continent.NorthAmerica, TwoLetterCode ="DM")]
        DMA = 59,

        [Country(Name = "Dominican Republic", Continent = Continent.NorthAmerica, TwoLetterCode ="DO")]
        DOM = 60,

        [Country(Name = "Algeria", Continent = Continent.Africa, TwoLetterCode ="DZ")]
        DZA = 61,

        [Country(Name = "Ecuador", Continent = Continent.SouthAmerica, TwoLetterCode ="EC")]
        ECU = 62,

        [Country(Name = "Estonia", Continent = Continent.Europe, TwoLetterCode ="EE")]
        EST = 63,

        [Country(Name = "Egypt", Continent = Continent.Africa, TwoLetterCode ="EG")]
        EGY = 64,

        [Country(Name = "Western Sahara", Continent = Continent.Africa, TwoLetterCode ="EH")]
        ESH = 65,

        [Country(Name = "Eritrea", Continent = Continent.Africa, TwoLetterCode ="ER")]
        ERI = 66,

        [Country(Name = "Spain", Continent = Continent.Europe, TwoLetterCode ="ES")]
        ESP = 67,

        [Country(Name = "Ethiopia", Continent = Continent.Africa, TwoLetterCode ="ET")]
        ETH = 68,

        [Country(Name = "Finland", Continent = Continent.Europe, TwoLetterCode ="FI")]
        FIN = 69,

        [Country(Name = "Fiji", Continent = Continent.Oceania, TwoLetterCode ="FJ")]
        FJI = 70,

        [Country(Name = "Falkland Islands", Continent = Continent.SouthAmerica, TwoLetterCode ="FK")]
        FLK = 71,

        [Country(Name = "Micronesia", Continent = Continent.Oceania, TwoLetterCode ="FM")]
        FSM = 72,

        [Country(Name = "Faroe Islands", Continent = Continent.Europe, TwoLetterCode ="FO")]
        FRO = 73,

        [Country(Name = "France", Continent = Continent.Europe, TwoLetterCode ="FR")]
        FRA = 74,

        [Country(Name = "Gabon", Continent = Continent.Africa, TwoLetterCode ="GA")]
        GAB = 75,

        [Country(Name = "United Kingdom", Continent = Continent.Europe, TwoLetterCode ="GB")]
        GBR = 76,

        [Country(Name = "Grenada", Continent = Continent.NorthAmerica, TwoLetterCode ="GD")]
        GRD = 77,

        [Country(Name = "Georgia", Continent = Continent.Asia, TwoLetterCode ="GE")]
        GEO = 78,

        [Country(Name = "French Guiana", Continent = Continent.SouthAmerica, TwoLetterCode ="GF")]
        GUF = 79,

        [Country(Name = "Guernsey", Continent = Continent.Europe, TwoLetterCode ="GG")]
        GGY = 80,

        [Country(Name = "Ghana", Continent = Continent.Africa, TwoLetterCode ="GH")]
        GHA = 81,

        [Country(Name = "Gibraltar", Continent = Continent.Europe, TwoLetterCode ="GI")]
        GIB = 82,

        [Country(Name = "Greenland", Continent = Continent.NorthAmerica, TwoLetterCode ="GL")]
        GRL = 83,

        [Country(Name = "Gambia", Continent = Continent.Africa, TwoLetterCode ="GM")]
        GMB = 84,

        [Country(Name = "Guinea", Continent = Continent.Africa, TwoLetterCode ="GN")]
        GIN = 85,

        [Country(Name = "Guadeloupe", Continent = Continent.NorthAmerica, TwoLetterCode ="GP")]
        GLP = 86,

        [Country(Name = "Equatorial Guinea", Continent = Continent.Africa, TwoLetterCode ="GQ")]
        GNQ = 87,

        [Country(Name = "Greece", Continent = Continent.Europe, TwoLetterCode ="GR")]
        GRC = 88,

        [Country(Name = "South Georgia and the South Sandwich Islands", Continent = Continent.Antarctica, TwoLetterCode ="GS")]
        SGS = 89,

        [Country(Name = "Guatemala", Continent = Continent.NorthAmerica, TwoLetterCode ="GT")]
        GTM = 90,

        [Country(Name = "Guam", Continent = Continent.Oceania, TwoLetterCode ="GU")]
        GUM = 91,

        [Country(Name = "Guinea-Bissau", Continent = Continent.Africa, TwoLetterCode ="GW")]
        GNB = 92,

        [Country(Name = "Guyana", Continent = Continent.SouthAmerica, TwoLetterCode ="GY")]
        GUY = 93,

        [Country(Name = "Hong Kong", Continent = Continent.Asia, TwoLetterCode ="HK")]
        HKG = 94,

        [Country(Name = "Heard Island and McDonald Islands", Continent = Continent.Antarctica, TwoLetterCode ="HM")]
        HMD = 95,

        [Country(Name = "Honduras", Continent = Continent.NorthAmerica, TwoLetterCode ="HN")]
        HND = 96,

        [Country(Name = "Croatia", Continent = Continent.Europe, TwoLetterCode ="HR")]
        HRV = 97,

        [Country(Name = "Haiti", Continent = Continent.NorthAmerica, TwoLetterCode ="HT")]
        HTI = 98,

        [Country(Name = "Hungary", Continent = Continent.Europe, TwoLetterCode ="HU")]
        HUN = 99,

        [Country(Name = "Indonesia", Continent = Continent.Asia, TwoLetterCode ="ID")]
        IDN = 100,

        [Country(Name = "Ireland", Continent = Continent.Europe, TwoLetterCode ="IE")]
        IRL = 101,

        [Country(Name = "Israel", Continent = Continent.Asia, TwoLetterCode ="IL")]
        ISR = 102,

        [Country(Name = "Isle of Man", Continent = Continent.Europe, TwoLetterCode ="IM")]
        IMN = 103,

        [Country(Name = "India", Continent = Continent.Asia, TwoLetterCode ="IN")]
        IND = 104,

        [Country(Name = "British Indian Ocean Territory", Continent = Continent.Asia, TwoLetterCode ="IO")]
        IOT = 105,

        [Country(Name = "Iraq", Continent = Continent.Asia, TwoLetterCode ="IQ")]
        IRQ = 106,

        [Country(Name = "Iran", Continent = Continent.Asia, TwoLetterCode ="IR")]
        IRN = 107,

        [Country(Name = "Iceland", Continent = Continent.Europe, TwoLetterCode ="IS")]
        ISL = 108,

        [Country(Name = "Italy", Continent = Continent.Europe, TwoLetterCode ="IT")]
        ITA = 109,

        [Country(Name = "Jersey", Continent = Continent.Europe, TwoLetterCode ="JE")]
        JEY = 110,

        [Country(Name = "Jamaica", Continent = Continent.NorthAmerica, TwoLetterCode ="JM")]
        JAM = 111,

        [Country(Name = "Jordan", Continent = Continent.Asia, TwoLetterCode ="JO")]
        JOR = 112,

        [Country(Name = "Japan", Continent = Continent.Asia, TwoLetterCode ="JP")]
        JPN = 113,

        [Country(Name = "Kenya", Continent = Continent.Africa, TwoLetterCode ="KE")]
        KEN = 114,

        [Country(Name = "Kyrgyzstan", Continent = Continent.Asia, TwoLetterCode ="KG")]
        KGZ = 115,

        [Country(Name = "Cambodia", Continent = Continent.Asia, TwoLetterCode ="KH")]
        KHM = 116,

        [Country(Name = "Kiribati", Continent = Continent.Oceania, TwoLetterCode ="KI")]
        KIR = 117,

        [Country(Name = "Comoros", Continent = Continent.Africa, TwoLetterCode ="KM")]
        COM = 118,

        [Country(Name = "Saint Kitts and Nevis", Continent = Continent.NorthAmerica, TwoLetterCode ="KN")]
        KNA = 119,

        [Country(Name = "North Korea", Continent = Continent.Asia, TwoLetterCode ="KP")]
        PRK = 120,

        [Country(Name = "South Korea", Continent = Continent.Asia, TwoLetterCode ="KR")]
        KOR = 121,

        [Country(Name = "Kosovo", Continent = Continent.Europe, TwoLetterCode ="XK")]
        XKX = 122,

        [Country(Name = "Kuwait", Continent = Continent.Asia, TwoLetterCode ="KW")]
        KWT = 123,

        [Country(Name = "Cayman Islands", Continent = Continent.NorthAmerica, TwoLetterCode ="KY")]
        CYM = 124,

        [Country(Name = "Kazakhstan", Continent = Continent.Asia, TwoLetterCode ="KZ")]
        KAZ = 125,

        [Country(Name = "Laos", Continent = Continent.Asia, TwoLetterCode ="LA")]
        LAO = 126,

        [Country(Name = "Lebanon", Continent = Continent.Asia, TwoLetterCode ="LB")]
        LBN = 127,

        [Country(Name = "Saint Lucia", Continent = Continent.NorthAmerica, TwoLetterCode ="LC")]
        LCA = 128,

        [Country(Name = "Liechtenstein", Continent = Continent.Europe, TwoLetterCode ="LI")]
        LIE = 129,

        [Country(Name = "Sri Lanka", Continent = Continent.Asia, TwoLetterCode ="LK")]
        LKA = 130,

        [Country(Name = "Liberia", Continent = Continent.Africa, TwoLetterCode ="LR")]
        LBR = 131,

        [Country(Name = "Lesotho", Continent = Continent.Africa, TwoLetterCode ="LS")]
        LSO = 132,

        [Country(Name = "Lithuania", Continent = Continent.Europe, TwoLetterCode ="LT")]
        LTU = 133,

        [Country(Name = "Luxembourg", Continent = Continent.Europe, TwoLetterCode ="LU")]
        LUX = 134,

        [Country(Name = "Latvia", Continent = Continent.Europe, TwoLetterCode ="LV")]
        LVA = 135,

        [Country(Name = "Libya", Continent = Continent.Africa, TwoLetterCode ="LY")]
        LBY = 136,

        [Country(Name = "Morocco", Continent = Continent.Africa, TwoLetterCode ="MA")]
        MAR = 137,

        [Country(Name = "Monaco", Continent = Continent.Europe, TwoLetterCode ="MC")]
        MCO = 138,

        [Country(Name = "Moldova", Continent = Continent.Europe, TwoLetterCode ="MD")]
        MDA = 139,

        [Country(Name = "Montenegro", Continent = Continent.Europe, TwoLetterCode ="ME")]
        MNE = 140,

        [Country(Name = "Saint Martin", Continent = Continent.NorthAmerica, TwoLetterCode ="MF")]
        MAF = 141,

        [Country(Name = "Madagascar", Continent = Continent.Africa, TwoLetterCode ="MG")]
        MDG = 142,

        [Country(Name = "Marshall Islands", Continent = Continent.Oceania, TwoLetterCode ="MH")]
        MHL = 143,

        [Country(Name = "North Macedonia", Continent = Continent.Europe, TwoLetterCode ="MK")]
        MKD = 144,

        [Country(Name = "Mali", Continent = Continent.Africa, TwoLetterCode ="ML")]
        MLI = 145,

        [Country(Name = "Myanmar", Continent = Continent.Asia, TwoLetterCode ="MM")]
        MMR = 146,

        [Country(Name = "Mongolia", Continent = Continent.Asia, TwoLetterCode ="MN")]
        MNG = 147,

        [Country(Name = "Macao", Continent = Continent.Asia, TwoLetterCode ="MO")]
        MAC = 148,

        [Country(Name = "Northern Mariana Islands", Continent = Continent.Oceania, TwoLetterCode ="MP")]
        MNP = 149,

        [Country(Name = "Martinique", Continent = Continent.NorthAmerica, TwoLetterCode ="MQ")]
        MTQ = 150,

        [Country(Name = "Mauritania", Continent = Continent.Africa, TwoLetterCode ="MR")]
        MRT = 151,

        [Country(Name = "Montserrat", Continent = Continent.NorthAmerica, TwoLetterCode ="MS")]
        MSR = 152,

        [Country(Name = "Malta", Continent = Continent.Europe, TwoLetterCode ="MT")]
        MLT = 153,

        [Country(Name = "Mauritius", Continent = Continent.Africa, TwoLetterCode ="MU")]
        MUS = 154,

        [Country(Name = "Maldives", Continent = Continent.Asia, TwoLetterCode ="MV")]
        MDV = 155,

        [Country(Name = "Malawi", Continent = Continent.Africa, TwoLetterCode ="MW")]
        MWI = 156,

        [Country(Name = "Mexico", Continent = Continent.NorthAmerica, TwoLetterCode ="MX")]
        MEX = 157,

        [Country(Name = "Malaysia", Continent = Continent.Asia, TwoLetterCode ="MY")]
        MYS = 158,

        [Country(Name = "Mozambique", Continent = Continent.Africa, TwoLetterCode ="MZ")]
        MOZ = 159,

        [Country(Name = "Namibia", Continent = Continent.Africa, TwoLetterCode ="NA")]
        NAM = 160,

        [Country(Name = "New Caledonia", Continent = Continent.Oceania, TwoLetterCode ="NC")]
        NCL = 161,

        [Country(Name = "Niger", Continent = Continent.Africa, TwoLetterCode ="NE")]
        NER = 162,

        [Country(Name = "Norfolk Island", Continent = Continent.Oceania, TwoLetterCode ="NF")]
        NFK = 163,

        [Country(Name = "Nigeria", Continent = Continent.Africa, TwoLetterCode ="NG")]
        NGA = 164,

        [Country(Name = "Nicaragua", Continent = Continent.NorthAmerica, TwoLetterCode ="NI")]
        NIC = 165,

        [Country(Name = "Netherlands", Continent = Continent.Europe, TwoLetterCode ="NL")]
        NLD = 166,

        [Country(Name = "Norway", Continent = Continent.Europe, TwoLetterCode ="NO")]
        NOR = 167,

        [Country(Name = "Nepal", Continent = Continent.Asia, TwoLetterCode ="NP")]
        NPL = 168,

        [Country(Name = "Nauru", Continent = Continent.Oceania, TwoLetterCode ="NR")]
        NRU = 169,

        [Country(Name = "Niue", Continent = Continent.Oceania, TwoLetterCode ="NU")]
        NIU = 170,

        [Country(Name = "New Zealand", Continent = Continent.Oceania, TwoLetterCode ="NZ")]
        NZL = 171,

        [Country(Name = "Oman", Continent = Continent.Asia, TwoLetterCode ="OM")]
        OMN = 172,

        [Country(Name = "Panama", Continent = Continent.NorthAmerica, TwoLetterCode ="PA")]
        PAN = 173,

        [Country(Name = "Peru", Continent = Continent.SouthAmerica, TwoLetterCode ="PE")]
        PER = 174,

        [Country(Name = "French Polynesia", Continent = Continent.Oceania, TwoLetterCode ="PF")]
        PYF = 175,

        [Country(Name = "Papua New Guinea", Continent = Continent.Oceania, TwoLetterCode ="PG")]
        PNG = 176,

        [Country(Name = "Philippines", Continent = Continent.Asia, TwoLetterCode ="PH")]
        PHL = 177,

        [Country(Name = "Pakistan", Continent = Continent.Asia, TwoLetterCode ="PK")]
        PAK = 178,

        [Country(Name = "Poland", Continent = Continent.Europe, TwoLetterCode ="PL")]
        POL = 179,

        [Country(Name = "Saint Pierre and Miquelon", Continent = Continent.NorthAmerica, TwoLetterCode ="PM")]
        SPM = 180,

        [Country(Name = "Pitcairn", Continent = Continent.Oceania, TwoLetterCode ="PN")]
        PCN = 181,

        [Country(Name = "Puerto Rico", Continent = Continent.NorthAmerica, TwoLetterCode ="PR")]
        PRI = 182,

        [Country(Name = "Palestinian Territory", Continent = Continent.Asia, TwoLetterCode ="PS")]
        PSE = 183,

        [Country(Name = "Portugal", Continent = Continent.Europe, TwoLetterCode ="PT")]
        PRT = 184,

        [Country(Name = "Palau", Continent = Continent.Oceania, TwoLetterCode ="PW")]
        PLW = 185,

        [Country(Name = "Paraguay", Continent = Continent.SouthAmerica, TwoLetterCode ="PY")]
        PRY = 186,

        [Country(Name = "Qatar", Continent = Continent.Asia, TwoLetterCode ="QA")]
        QAT = 187,

        [Country(Name = "Reunion", Continent = Continent.Africa, TwoLetterCode ="RE")]
        REU = 188,

        [Country(Name = "Romania", Continent = Continent.Europe, TwoLetterCode ="RO")]
        ROU = 189,

        [Country(Name = "Serbia", Continent = Continent.Europe, TwoLetterCode ="RS")]
        SRB = 190,

        [Country(Name = "Russia", Continent = Continent.Europe, TwoLetterCode ="RU")]
        RUS = 191,

        [Country(Name = "Rwanda", Continent = Continent.Africa, TwoLetterCode ="RW")]
        RWA = 192,

        [Country(Name = "Saudi Arabia", Continent = Continent.Asia, TwoLetterCode ="SA")]
        SAU = 193,

        [Country(Name = "Solomon Islands", Continent = Continent.Oceania, TwoLetterCode ="SB")]
        SLB = 194,

        [Country(Name = "Seychelles", Continent = Continent.Africa, TwoLetterCode ="SC")]
        SYC = 195,

        [Country(Name = "Sudan", Continent = Continent.Africa, TwoLetterCode ="SD")]
        SDN = 196,

        [Country(Name = "South Sudan", Continent = Continent.Africa, TwoLetterCode ="SS")]
        SSD = 197,

        [Country(Name = "Sweden", Continent = Continent.Europe, TwoLetterCode ="SE")]
        SWE = 198,

        [Country(Name = "Singapore", Continent = Continent.Asia, TwoLetterCode ="SG")]
        SGP = 199,

        [Country(Name = "Saint Helena", Continent = Continent.Africa, TwoLetterCode ="SH")]
        SHN = 200,

        [Country(Name = "Slovenia", Continent = Continent.Europe, TwoLetterCode ="SI")]
        SVN = 201,

        [Country(Name = "Svalbard and Jan Mayen", Continent = Continent.Europe, TwoLetterCode ="SJ")]
        SJM = 202,

        [Country(Name = "Slovakia", Continent = Continent.Europe, TwoLetterCode ="SK")]
        SVK = 203,

        [Country(Name = "Sierra Leone", Continent = Continent.Africa, TwoLetterCode ="SL")]
        SLE = 204,

        [Country(Name = "San Marino", Continent = Continent.Europe, TwoLetterCode ="SM")]
        SMR = 205,

        [Country(Name = "Senegal", Continent = Continent.Africa, TwoLetterCode ="SN")]
        SEN = 206,

        [Country(Name = "Somalia", Continent = Continent.Africa, TwoLetterCode ="SO")]
        SOM = 207,

        [Country(Name = "Suriname", Continent = Continent.SouthAmerica, TwoLetterCode ="SR")]
        SUR = 208,

        [Country(Name = "Sao Tome and Principe", Continent = Continent.Africa, TwoLetterCode ="ST")]
        STP = 209,

        [Country(Name = "El Salvador", Continent = Continent.NorthAmerica, TwoLetterCode ="SV")]
        SLV = 210,

        [Country(Name = "Sint Maarten", Continent = Continent.NorthAmerica, TwoLetterCode ="SX")]
        SXM = 211,

        [Country(Name = "Syria", Continent = Continent.Asia, TwoLetterCode ="SY")]
        SYR = 212,

        [Country(Name = "Eswatini", Continent = Continent.Africa, TwoLetterCode ="SZ")]
        SWZ = 213,

        [Country(Name = "Turks and Caicos Islands", Continent = Continent.NorthAmerica, TwoLetterCode ="TC")]
        TCA = 214,

        [Country(Name = "Chad", Continent = Continent.Africa, TwoLetterCode ="TD")]
        TCD = 215,

        [Country(Name = "French Southern Territories", Continent = Continent.Antarctica, TwoLetterCode ="TF")]
        ATF = 216,

        [Country(Name = "Togo", Continent = Continent.Africa, TwoLetterCode ="TG")]
        TGO = 217,

        [Country(Name = "Thailand", Continent = Continent.Asia, TwoLetterCode ="TH")]
        THA = 218,

        [Country(Name = "Tajikistan", Continent = Continent.Asia, TwoLetterCode ="TJ")]
        TJK = 219,

        [Country(Name = "Tokelau", Continent = Continent.Oceania, TwoLetterCode ="TK")]
        TKL = 220,

        [Country(Name = "Timor Leste", Continent = Continent.Oceania, TwoLetterCode ="TL")]
        TLS = 221,

        [Country(Name = "Turkmenistan", Continent = Continent.Asia, TwoLetterCode ="TM")]
        TKM = 222,

        [Country(Name = "Tunisia", Continent = Continent.Africa, TwoLetterCode ="TN")]
        TUN = 223,

        [Country(Name = "Tonga", Continent = Continent.Oceania, TwoLetterCode ="TO")]
        TON = 224,

        [Country(Name = "Turkey", Continent = Continent.Asia, TwoLetterCode ="TR")]
        TUR = 225,

        [Country(Name = "Trinidad and Tobago", Continent = Continent.NorthAmerica, TwoLetterCode ="TT")]
        TTO = 226,

        [Country(Name = "Tuvalu", Continent = Continent.Oceania, TwoLetterCode ="TV")]
        TUV = 227,

        [Country(Name = "Taiwan", Continent = Continent.Asia, TwoLetterCode ="TW")]
        TWN = 228,

        [Country(Name = "Tanzania", Continent = Continent.Africa, TwoLetterCode ="TZ")]
        TZA = 229,

        [Country(Name = "Ukraine", Continent = Continent.Europe, TwoLetterCode ="UA")]
        UKR = 230,

        [Country(Name = "Uganda", Continent = Continent.Africa, TwoLetterCode ="UG")]
        UGA = 231,

        [Country(Name = "United States Minor Outlying Islands", Continent = Continent.Oceania, TwoLetterCode ="UM")]
        UMI = 232,

        [Country(Name = "United States", Continent = Continent.NorthAmerica, TwoLetterCode ="US")]
        USA = 233,

        [Country(Name = "Uruguay", Continent = Continent.SouthAmerica, TwoLetterCode ="UY")]
        URY = 234,

        [Country(Name = "Uzbekistan", Continent = Continent.Asia, TwoLetterCode ="UZ")]
        UZB = 235,

        [Country(Name = "Vatican", Continent = Continent.Europe, TwoLetterCode ="VA")]
        VAT = 236,

        [Country(Name = "Saint Vincent and the Grenadines", Continent = Continent.NorthAmerica, TwoLetterCode ="VC")]
        VCT = 237,

        [Country(Name = "Venezuela", Continent = Continent.SouthAmerica, TwoLetterCode ="VE")]
        VEN = 238,

        [Country(Name = "British Virgin Islands", Continent = Continent.NorthAmerica, TwoLetterCode ="VG")]
        VGB = 239,

        [Country(Name = "U.S. Virgin Islands", Continent = Continent.NorthAmerica, TwoLetterCode ="VI")]
        VIR = 240,

        [Country(Name = "Vietnam", Continent = Continent.Asia, TwoLetterCode ="VN")]
        VNM = 241,

        [Country(Name = "Vanuatu", Continent = Continent.Oceania, TwoLetterCode ="VU")]
        VUT = 242,

        [Country(Name = "Wallis and Futuna", Continent = Continent.Oceania, TwoLetterCode ="WF")]
        WLF = 243,

        [Country(Name = "Samoa", Continent = Continent.Oceania, TwoLetterCode ="WS")]
        WSM = 244,

        [Country(Name = "Yemen", Continent = Continent.Asia, TwoLetterCode ="YE")]
        YEM = 245,

        [Country(Name = "Mayotte", Continent = Continent.Africa, TwoLetterCode ="YT")]
        MYT = 246,

        [Country(Name = "South Africa", Continent = Continent.Africa, TwoLetterCode ="ZA")]
        ZAF = 247,

        [Country(Name = "Zambia", Continent = Continent.Africa, TwoLetterCode ="ZM")]
        ZMB = 248,

        [Country(Name = "Zimbabwe", Continent = Continent.Africa, TwoLetterCode ="ZW")]
        ZWE = 249,

        [Country(Name = "Serbia and Montenegro", Continent = Continent.Europe, TwoLetterCode ="CS")]
        SCG = 250,

        [Country(Name = "Netherlands Antilles", Continent = Continent.NorthAmerica, TwoLetterCode ="AN")]
        ANT = 251,

        [Country(Name = "Unknown", Continent = Continent.Unknown, TwoLetterCode ="UK")]
        UNK = 252,
    }

    /// <summary>
    /// Contains a list of all continents for the countries.
    /// </summary>
    public enum Continent
    {
        Unknown,
        Africa,
        Antarctica,
        Asia,
        Europe,
        NorthAmerica,
        Oceania,
        SouthAmerica
    }

    public class CountryAttribute : Attribute
    {
        public string Name          { get; set; }
        public Continent Continent  { get; set; }
        public string TwoLetterCode { get; set; }
    }
    
    public static class CountryDtoExtensions
    {
        private static Dictionary<CountryDto, CountryAttribute> _countryDtoMap;
        private static Dictionary<string, CountryDto> _shortNameToCountryDto = new Dictionary<string, CountryDto>(StringComparer.OrdinalIgnoreCase);

        static CountryDtoExtensions()
        {
            _countryDtoMap = new Dictionary<CountryDto, CountryAttribute>();
            var type = typeof(CountryDto);

            foreach (var countryDto in Enum.GetValues<CountryDto>())
            {
                var memInfo     = type.GetMember(countryDto.ToString());
                var attributes  = memInfo[0].GetCustomAttributes(typeof(DisplayAttribute), false);
                var displayAttr = (CountryAttribute) attributes[0];

                _countryDtoMap[countryDto] = displayAttr;
                _shortNameToCountryDto[displayAttr.TwoLetterCode] = countryDto;
            }
        }

        /// <summary>
        /// Gets a CountryAttribute which describes an individual country.
        /// </summary>
        public static CountryAttribute GetDisplayAttribute(this CountryDto countryDto) => _countryDtoMap[countryDto];

        /// <summary>
        /// Gets a country from a 2 letter country code.
        /// </summary>
        public static CountryDto GetCountryDtoFromTwoLetterCode(this string shortName)
        {
            if (_shortNameToCountryDto.TryGetValue(shortName, out var countryDto))
                return countryDto;

            return CountryDto.UNK;
        }
    }
}