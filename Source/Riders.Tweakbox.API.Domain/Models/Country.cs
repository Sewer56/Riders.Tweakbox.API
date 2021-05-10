using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Riders.Tweakbox.API.Domain.Models
{
    public enum Country
    {
        [Display(Name = "Andorra", GroupName = "EU", Description = "020", ShortName = "AD")]
        AND = 0,

        [Display(Name = "United Arab Emirates", GroupName = "AS", Description = "784", ShortName = "AE")]
        ARE = 1,

        [Display(Name = "Afghanistan", GroupName = "AS", Description = "004", ShortName = "AF")]
        AFG = 2,

        [Display(Name = "Antigua and Barbuda", GroupName = "NA", Description = "028", ShortName = "AG")]
        ATG = 3,

        [Display(Name = "Anguilla", GroupName = "NA", Description = "660", ShortName = "AI")]
        AIA = 4,

        [Display(Name = "Albania", GroupName = "EU", Description = "008", ShortName = "AL")]
        ALB = 5,

        [Display(Name = "Armenia", GroupName = "AS", Description = "051", ShortName = "AM")]
        ARM = 6,

        [Display(Name = "Angola", GroupName = "AF", Description = "024", ShortName = "AO")]
        AGO = 7,

        [Display(Name = "Antarctica", GroupName = "AN", Description = "010", ShortName = "AQ")]
        ATA = 8,

        [Display(Name = "Argentina", GroupName = "SA", Description = "032", ShortName = "AR")]
        ARG = 9,

        [Display(Name = "American Samoa", GroupName = "OC", Description = "016", ShortName = "AS")]
        ASM = 10,

        [Display(Name = "Austria", GroupName = "EU", Description = "040", ShortName = "AT")]
        AUT = 11,

        [Display(Name = "Australia", GroupName = "OC", Description = "036", ShortName = "AU")]
        AUS = 12,

        [Display(Name = "Aruba", GroupName = "NA", Description = "533", ShortName = "AW")]
        ABW = 13,

        [Display(Name = "Aland Islands", GroupName = "EU", Description = "248", ShortName = "AX")]
        ALA = 14,

        [Display(Name = "Azerbaijan", GroupName = "AS", Description = "031", ShortName = "AZ")]
        AZE = 15,

        [Display(Name = "Bosnia and Herzegovina", GroupName = "EU", Description = "070", ShortName = "BA")]
        BIH = 16,

        [Display(Name = "Barbados", GroupName = "NA", Description = "052", ShortName = "BB")]
        BRB = 17,

        [Display(Name = "Bangladesh", GroupName = "AS", Description = "050", ShortName = "BD")]
        BGD = 18,

        [Display(Name = "Belgium", GroupName = "EU", Description = "056", ShortName = "BE")]
        BEL = 19,

        [Display(Name = "Burkina Faso", GroupName = "AF", Description = "854", ShortName = "BF")]
        BFA = 20,

        [Display(Name = "Bulgaria", GroupName = "EU", Description = "100", ShortName = "BG")]
        BGR = 21,

        [Display(Name = "Bahrain", GroupName = "AS", Description = "048", ShortName = "BH")]
        BHR = 22,

        [Display(Name = "Burundi", GroupName = "AF", Description = "108", ShortName = "BI")]
        BDI = 23,

        [Display(Name = "Benin", GroupName = "AF", Description = "204", ShortName = "BJ")]
        BEN = 24,

        [Display(Name = "Saint Barthelemy", GroupName = "NA", Description = "652", ShortName = "BL")]
        BLM = 25,

        [Display(Name = "Bermuda", GroupName = "NA", Description = "060", ShortName = "BM")]
        BMU = 26,

        [Display(Name = "Brunei", GroupName = "AS", Description = "096", ShortName = "BN")]
        BRN = 27,

        [Display(Name = "Bolivia", GroupName = "SA", Description = "068", ShortName = "BO")]
        BOL = 28,

        [Display(Name = "Bonaire, Saint Eustatius and Saba ", GroupName = "NA", Description = "535", ShortName = "BQ")]
        BES = 29,

        [Display(Name = "Brazil", GroupName = "SA", Description = "076", ShortName = "BR")]
        BRA = 30,

        [Display(Name = "Bahamas", GroupName = "NA", Description = "044", ShortName = "BS")]
        BHS = 31,

        [Display(Name = "Bhutan", GroupName = "AS", Description = "064", ShortName = "BT")]
        BTN = 32,

        [Display(Name = "Bouvet Island", GroupName = "AN", Description = "074", ShortName = "BV")]
        BVT = 33,

        [Display(Name = "Botswana", GroupName = "AF", Description = "072", ShortName = "BW")]
        BWA = 34,

        [Display(Name = "Belarus", GroupName = "EU", Description = "112", ShortName = "BY")]
        BLR = 35,

        [Display(Name = "Belize", GroupName = "NA", Description = "084", ShortName = "BZ")]
        BLZ = 36,

        [Display(Name = "Canada", GroupName = "NA", Description = "124", ShortName = "CA")]
        CAN = 37,

        [Display(Name = "Cocos Islands", GroupName = "AS", Description = "166", ShortName = "CC")]
        CCK = 38,

        [Display(Name = "Democratic Republic of the Congo", GroupName = "AF", Description = "180", ShortName = "CD")]
        COD = 39,

        [Display(Name = "Central African Republic", GroupName = "AF", Description = "140", ShortName = "CF")]
        CAF = 40,

        [Display(Name = "Republic of the Congo", GroupName = "AF", Description = "178", ShortName = "CG")]
        COG = 41,

        [Display(Name = "Switzerland", GroupName = "EU", Description = "756", ShortName = "CH")]
        CHE = 42,

        [Display(Name = "Ivory Coast", GroupName = "AF", Description = "384", ShortName = "CI")]
        CIV = 43,

        [Display(Name = "Cook Islands", GroupName = "OC", Description = "184", ShortName = "CK")]
        COK = 44,

        [Display(Name = "Chile", GroupName = "SA", Description = "152", ShortName = "CL")]
        CHL = 45,

        [Display(Name = "Cameroon", GroupName = "AF", Description = "120", ShortName = "CM")]
        CMR = 46,

        [Display(Name = "China", GroupName = "AS", Description = "156", ShortName = "CN")]
        CHN = 47,

        [Display(Name = "Colombia", GroupName = "SA", Description = "170", ShortName = "CO")]
        COL = 48,

        [Display(Name = "Costa Rica", GroupName = "NA", Description = "188", ShortName = "CR")]
        CRI = 49,

        [Display(Name = "Cuba", GroupName = "NA", Description = "192", ShortName = "CU")]
        CUB = 50,

        [Display(Name = "Cabo Verde", GroupName = "AF", Description = "132", ShortName = "CV")]
        CPV = 51,

        [Display(Name = "Curacao", GroupName = "NA", Description = "531", ShortName = "CW")]
        CUW = 52,

        [Display(Name = "Christmas Island", GroupName = "OC", Description = "162", ShortName = "CX")]
        CXR = 53,

        [Display(Name = "Cyprus", GroupName = "EU", Description = "196", ShortName = "CY")]
        CYP = 54,

        [Display(Name = "Czechia", GroupName = "EU", Description = "203", ShortName = "CZ")]
        CZE = 55,

        [Display(Name = "Germany", GroupName = "EU", Description = "276", ShortName = "DE")]
        DEU = 56,

        [Display(Name = "Djibouti", GroupName = "AF", Description = "262", ShortName = "DJ")]
        DJI = 57,

        [Display(Name = "Denmark", GroupName = "EU", Description = "208", ShortName = "DK")]
        DNK = 58,

        [Display(Name = "Dominica", GroupName = "NA", Description = "212", ShortName = "DM")]
        DMA = 59,

        [Display(Name = "Dominican Republic", GroupName = "NA", Description = "214", ShortName = "DO")]
        DOM = 60,

        [Display(Name = "Algeria", GroupName = "AF", Description = "012", ShortName = "DZ")]
        DZA = 61,

        [Display(Name = "Ecuador", GroupName = "SA", Description = "218", ShortName = "EC")]
        ECU = 62,

        [Display(Name = "Estonia", GroupName = "EU", Description = "233", ShortName = "EE")]
        EST = 63,

        [Display(Name = "Egypt", GroupName = "AF", Description = "818", ShortName = "EG")]
        EGY = 64,

        [Display(Name = "Western Sahara", GroupName = "AF", Description = "732", ShortName = "EH")]
        ESH = 65,

        [Display(Name = "Eritrea", GroupName = "AF", Description = "232", ShortName = "ER")]
        ERI = 66,

        [Display(Name = "Spain", GroupName = "EU", Description = "724", ShortName = "ES")]
        ESP = 67,

        [Display(Name = "Ethiopia", GroupName = "AF", Description = "231", ShortName = "ET")]
        ETH = 68,

        [Display(Name = "Finland", GroupName = "EU", Description = "246", ShortName = "FI")]
        FIN = 69,

        [Display(Name = "Fiji", GroupName = "OC", Description = "242", ShortName = "FJ")]
        FJI = 70,

        [Display(Name = "Falkland Islands", GroupName = "SA", Description = "238", ShortName = "FK")]
        FLK = 71,

        [Display(Name = "Micronesia", GroupName = "OC", Description = "583", ShortName = "FM")]
        FSM = 72,

        [Display(Name = "Faroe Islands", GroupName = "EU", Description = "234", ShortName = "FO")]
        FRO = 73,

        [Display(Name = "France", GroupName = "EU", Description = "250", ShortName = "FR")]
        FRA = 74,

        [Display(Name = "Gabon", GroupName = "AF", Description = "266", ShortName = "GA")]
        GAB = 75,

        [Display(Name = "United Kingdom", GroupName = "EU", Description = "826", ShortName = "GB")]
        GBR = 76,

        [Display(Name = "Grenada", GroupName = "NA", Description = "308", ShortName = "GD")]
        GRD = 77,

        [Display(Name = "Georgia", GroupName = "AS", Description = "268", ShortName = "GE")]
        GEO = 78,

        [Display(Name = "French Guiana", GroupName = "SA", Description = "254", ShortName = "GF")]
        GUF = 79,

        [Display(Name = "Guernsey", GroupName = "EU", Description = "831", ShortName = "GG")]
        GGY = 80,

        [Display(Name = "Ghana", GroupName = "AF", Description = "288", ShortName = "GH")]
        GHA = 81,

        [Display(Name = "Gibraltar", GroupName = "EU", Description = "292", ShortName = "GI")]
        GIB = 82,

        [Display(Name = "Greenland", GroupName = "NA", Description = "304", ShortName = "GL")]
        GRL = 83,

        [Display(Name = "Gambia", GroupName = "AF", Description = "270", ShortName = "GM")]
        GMB = 84,

        [Display(Name = "Guinea", GroupName = "AF", Description = "324", ShortName = "GN")]
        GIN = 85,

        [Display(Name = "Guadeloupe", GroupName = "NA", Description = "312", ShortName = "GP")]
        GLP = 86,

        [Display(Name = "Equatorial Guinea", GroupName = "AF", Description = "226", ShortName = "GQ")]
        GNQ = 87,

        [Display(Name = "Greece", GroupName = "EU", Description = "300", ShortName = "GR")]
        GRC = 88,

        [Display(Name = "South Georgia and the South Sandwich Islands", GroupName = "AN", Description = "239",
            ShortName = "GS")]
        SGS = 89,

        [Display(Name = "Guatemala", GroupName = "NA", Description = "320", ShortName = "GT")]
        GTM = 90,

        [Display(Name = "Guam", GroupName = "OC", Description = "316", ShortName = "GU")]
        GUM = 91,

        [Display(Name = "Guinea-Bissau", GroupName = "AF", Description = "624", ShortName = "GW")]
        GNB = 92,

        [Display(Name = "Guyana", GroupName = "SA", Description = "328", ShortName = "GY")]
        GUY = 93,

        [Display(Name = "Hong Kong", GroupName = "AS", Description = "344", ShortName = "HK")]
        HKG = 94,

        [Display(Name = "Heard Island and McDonald Islands", GroupName = "AN", Description = "334", ShortName = "HM")]
        HMD = 95,

        [Display(Name = "Honduras", GroupName = "NA", Description = "340", ShortName = "HN")]
        HND = 96,

        [Display(Name = "Croatia", GroupName = "EU", Description = "191", ShortName = "HR")]
        HRV = 97,

        [Display(Name = "Haiti", GroupName = "NA", Description = "332", ShortName = "HT")]
        HTI = 98,

        [Display(Name = "Hungary", GroupName = "EU", Description = "348", ShortName = "HU")]
        HUN = 99,

        [Display(Name = "Indonesia", GroupName = "AS", Description = "360", ShortName = "ID")]
        IDN = 100,

        [Display(Name = "Ireland", GroupName = "EU", Description = "372", ShortName = "IE")]
        IRL = 101,

        [Display(Name = "Israel", GroupName = "AS", Description = "376", ShortName = "IL")]
        ISR = 102,

        [Display(Name = "Isle of Man", GroupName = "EU", Description = "833", ShortName = "IM")]
        IMN = 103,

        [Display(Name = "India", GroupName = "AS", Description = "356", ShortName = "IN")]
        IND = 104,

        [Display(Name = "British Indian Ocean Territory", GroupName = "AS", Description = "086", ShortName = "IO")]
        IOT = 105,

        [Display(Name = "Iraq", GroupName = "AS", Description = "368", ShortName = "IQ")]
        IRQ = 106,

        [Display(Name = "Iran", GroupName = "AS", Description = "364", ShortName = "IR")]
        IRN = 107,

        [Display(Name = "Iceland", GroupName = "EU", Description = "352", ShortName = "IS")]
        ISL = 108,

        [Display(Name = "Italy", GroupName = "EU", Description = "380", ShortName = "IT")]
        ITA = 109,

        [Display(Name = "Jersey", GroupName = "EU", Description = "832", ShortName = "JE")]
        JEY = 110,

        [Display(Name = "Jamaica", GroupName = "NA", Description = "388", ShortName = "JM")]
        JAM = 111,

        [Display(Name = "Jordan", GroupName = "AS", Description = "400", ShortName = "JO")]
        JOR = 112,

        [Display(Name = "Japan", GroupName = "AS", Description = "392", ShortName = "JP")]
        JPN = 113,

        [Display(Name = "Kenya", GroupName = "AF", Description = "404", ShortName = "KE")]
        KEN = 114,

        [Display(Name = "Kyrgyzstan", GroupName = "AS", Description = "417", ShortName = "KG")]
        KGZ = 115,

        [Display(Name = "Cambodia", GroupName = "AS", Description = "116", ShortName = "KH")]
        KHM = 116,

        [Display(Name = "Kiribati", GroupName = "OC", Description = "296", ShortName = "KI")]
        KIR = 117,

        [Display(Name = "Comoros", GroupName = "AF", Description = "174", ShortName = "KM")]
        COM = 118,

        [Display(Name = "Saint Kitts and Nevis", GroupName = "NA", Description = "659", ShortName = "KN")]
        KNA = 119,

        [Display(Name = "North Korea", GroupName = "AS", Description = "408", ShortName = "KP")]
        PRK = 120,

        [Display(Name = "South Korea", GroupName = "AS", Description = "410", ShortName = "KR")]
        KOR = 121,

        [Display(Name = "Kosovo", GroupName = "EU", Description = "0", ShortName = "XK")]
        XKX = 122,

        [Display(Name = "Kuwait", GroupName = "AS", Description = "414", ShortName = "KW")]
        KWT = 123,

        [Display(Name = "Cayman Islands", GroupName = "NA", Description = "136", ShortName = "KY")]
        CYM = 124,

        [Display(Name = "Kazakhstan", GroupName = "AS", Description = "398", ShortName = "KZ")]
        KAZ = 125,

        [Display(Name = "Laos", GroupName = "AS", Description = "418", ShortName = "LA")]
        LAO = 126,

        [Display(Name = "Lebanon", GroupName = "AS", Description = "422", ShortName = "LB")]
        LBN = 127,

        [Display(Name = "Saint Lucia", GroupName = "NA", Description = "662", ShortName = "LC")]
        LCA = 128,

        [Display(Name = "Liechtenstein", GroupName = "EU", Description = "438", ShortName = "LI")]
        LIE = 129,

        [Display(Name = "Sri Lanka", GroupName = "AS", Description = "144", ShortName = "LK")]
        LKA = 130,

        [Display(Name = "Liberia", GroupName = "AF", Description = "430", ShortName = "LR")]
        LBR = 131,

        [Display(Name = "Lesotho", GroupName = "AF", Description = "426", ShortName = "LS")]
        LSO = 132,

        [Display(Name = "Lithuania", GroupName = "EU", Description = "440", ShortName = "LT")]
        LTU = 133,

        [Display(Name = "Luxembourg", GroupName = "EU", Description = "442", ShortName = "LU")]
        LUX = 134,

        [Display(Name = "Latvia", GroupName = "EU", Description = "428", ShortName = "LV")]
        LVA = 135,

        [Display(Name = "Libya", GroupName = "AF", Description = "434", ShortName = "LY")]
        LBY = 136,

        [Display(Name = "Morocco", GroupName = "AF", Description = "504", ShortName = "MA")]
        MAR = 137,

        [Display(Name = "Monaco", GroupName = "EU", Description = "492", ShortName = "MC")]
        MCO = 138,

        [Display(Name = "Moldova", GroupName = "EU", Description = "498", ShortName = "MD")]
        MDA = 139,

        [Display(Name = "Montenegro", GroupName = "EU", Description = "499", ShortName = "ME")]
        MNE = 140,

        [Display(Name = "Saint Martin", GroupName = "NA", Description = "663", ShortName = "MF")]
        MAF = 141,

        [Display(Name = "Madagascar", GroupName = "AF", Description = "450", ShortName = "MG")]
        MDG = 142,

        [Display(Name = "Marshall Islands", GroupName = "OC", Description = "584", ShortName = "MH")]
        MHL = 143,

        [Display(Name = "North Macedonia", GroupName = "EU", Description = "807", ShortName = "MK")]
        MKD = 144,

        [Display(Name = "Mali", GroupName = "AF", Description = "466", ShortName = "ML")]
        MLI = 145,

        [Display(Name = "Myanmar", GroupName = "AS", Description = "104", ShortName = "MM")]
        MMR = 146,

        [Display(Name = "Mongolia", GroupName = "AS", Description = "496", ShortName = "MN")]
        MNG = 147,

        [Display(Name = "Macao", GroupName = "AS", Description = "446", ShortName = "MO")]
        MAC = 148,

        [Display(Name = "Northern Mariana Islands", GroupName = "OC", Description = "580", ShortName = "MP")]
        MNP = 149,

        [Display(Name = "Martinique", GroupName = "NA", Description = "474", ShortName = "MQ")]
        MTQ = 150,

        [Display(Name = "Mauritania", GroupName = "AF", Description = "478", ShortName = "MR")]
        MRT = 151,

        [Display(Name = "Montserrat", GroupName = "NA", Description = "500", ShortName = "MS")]
        MSR = 152,

        [Display(Name = "Malta", GroupName = "EU", Description = "470", ShortName = "MT")]
        MLT = 153,

        [Display(Name = "Mauritius", GroupName = "AF", Description = "480", ShortName = "MU")]
        MUS = 154,

        [Display(Name = "Maldives", GroupName = "AS", Description = "462", ShortName = "MV")]
        MDV = 155,

        [Display(Name = "Malawi", GroupName = "AF", Description = "454", ShortName = "MW")]
        MWI = 156,

        [Display(Name = "Mexico", GroupName = "NA", Description = "484", ShortName = "MX")]
        MEX = 157,

        [Display(Name = "Malaysia", GroupName = "AS", Description = "458", ShortName = "MY")]
        MYS = 158,

        [Display(Name = "Mozambique", GroupName = "AF", Description = "508", ShortName = "MZ")]
        MOZ = 159,

        [Display(Name = "Namibia", GroupName = "AF", Description = "516", ShortName = "NA")]
        NAM = 160,

        [Display(Name = "New Caledonia", GroupName = "OC", Description = "540", ShortName = "NC")]
        NCL = 161,

        [Display(Name = "Niger", GroupName = "AF", Description = "562", ShortName = "NE")]
        NER = 162,

        [Display(Name = "Norfolk Island", GroupName = "OC", Description = "574", ShortName = "NF")]
        NFK = 163,

        [Display(Name = "Nigeria", GroupName = "AF", Description = "566", ShortName = "NG")]
        NGA = 164,

        [Display(Name = "Nicaragua", GroupName = "NA", Description = "558", ShortName = "NI")]
        NIC = 165,

        [Display(Name = "Netherlands", GroupName = "EU", Description = "528", ShortName = "NL")]
        NLD = 166,

        [Display(Name = "Norway", GroupName = "EU", Description = "578", ShortName = "NO")]
        NOR = 167,

        [Display(Name = "Nepal", GroupName = "AS", Description = "524", ShortName = "NP")]
        NPL = 168,

        [Display(Name = "Nauru", GroupName = "OC", Description = "520", ShortName = "NR")]
        NRU = 169,

        [Display(Name = "Niue", GroupName = "OC", Description = "570", ShortName = "NU")]
        NIU = 170,

        [Display(Name = "New Zealand", GroupName = "OC", Description = "554", ShortName = "NZ")]
        NZL = 171,

        [Display(Name = "Oman", GroupName = "AS", Description = "512", ShortName = "OM")]
        OMN = 172,

        [Display(Name = "Panama", GroupName = "NA", Description = "591", ShortName = "PA")]
        PAN = 173,

        [Display(Name = "Peru", GroupName = "SA", Description = "604", ShortName = "PE")]
        PER = 174,

        [Display(Name = "French Polynesia", GroupName = "OC", Description = "258", ShortName = "PF")]
        PYF = 175,

        [Display(Name = "Papua New Guinea", GroupName = "OC", Description = "598", ShortName = "PG")]
        PNG = 176,

        [Display(Name = "Philippines", GroupName = "AS", Description = "608", ShortName = "PH")]
        PHL = 177,

        [Display(Name = "Pakistan", GroupName = "AS", Description = "586", ShortName = "PK")]
        PAK = 178,

        [Display(Name = "Poland", GroupName = "EU", Description = "616", ShortName = "PL")]
        POL = 179,

        [Display(Name = "Saint Pierre and Miquelon", GroupName = "NA", Description = "666", ShortName = "PM")]
        SPM = 180,

        [Display(Name = "Pitcairn", GroupName = "OC", Description = "612", ShortName = "PN")]
        PCN = 181,

        [Display(Name = "Puerto Rico", GroupName = "NA", Description = "630", ShortName = "PR")]
        PRI = 182,

        [Display(Name = "Palestinian Territory", GroupName = "AS", Description = "275", ShortName = "PS")]
        PSE = 183,

        [Display(Name = "Portugal", GroupName = "EU", Description = "620", ShortName = "PT")]
        PRT = 184,

        [Display(Name = "Palau", GroupName = "OC", Description = "585", ShortName = "PW")]
        PLW = 185,

        [Display(Name = "Paraguay", GroupName = "SA", Description = "600", ShortName = "PY")]
        PRY = 186,

        [Display(Name = "Qatar", GroupName = "AS", Description = "634", ShortName = "QA")]
        QAT = 187,

        [Display(Name = "Reunion", GroupName = "AF", Description = "638", ShortName = "RE")]
        REU = 188,

        [Display(Name = "Romania", GroupName = "EU", Description = "642", ShortName = "RO")]
        ROU = 189,

        [Display(Name = "Serbia", GroupName = "EU", Description = "688", ShortName = "RS")]
        SRB = 190,

        [Display(Name = "Russia", GroupName = "EU", Description = "643", ShortName = "RU")]
        RUS = 191,

        [Display(Name = "Rwanda", GroupName = "AF", Description = "646", ShortName = "RW")]
        RWA = 192,

        [Display(Name = "Saudi Arabia", GroupName = "AS", Description = "682", ShortName = "SA")]
        SAU = 193,

        [Display(Name = "Solomon Islands", GroupName = "OC", Description = "090", ShortName = "SB")]
        SLB = 194,

        [Display(Name = "Seychelles", GroupName = "AF", Description = "690", ShortName = "SC")]
        SYC = 195,

        [Display(Name = "Sudan", GroupName = "AF", Description = "729", ShortName = "SD")]
        SDN = 196,

        [Display(Name = "South Sudan", GroupName = "AF", Description = "728", ShortName = "SS")]
        SSD = 197,

        [Display(Name = "Sweden", GroupName = "EU", Description = "752", ShortName = "SE")]
        SWE = 198,

        [Display(Name = "Singapore", GroupName = "AS", Description = "702", ShortName = "SG")]
        SGP = 199,

        [Display(Name = "Saint Helena", GroupName = "AF", Description = "654", ShortName = "SH")]
        SHN = 200,

        [Display(Name = "Slovenia", GroupName = "EU", Description = "705", ShortName = "SI")]
        SVN = 201,

        [Display(Name = "Svalbard and Jan Mayen", GroupName = "EU", Description = "744", ShortName = "SJ")]
        SJM = 202,

        [Display(Name = "Slovakia", GroupName = "EU", Description = "703", ShortName = "SK")]
        SVK = 203,

        [Display(Name = "Sierra Leone", GroupName = "AF", Description = "694", ShortName = "SL")]
        SLE = 204,

        [Display(Name = "San Marino", GroupName = "EU", Description = "674", ShortName = "SM")]
        SMR = 205,

        [Display(Name = "Senegal", GroupName = "AF", Description = "686", ShortName = "SN")]
        SEN = 206,

        [Display(Name = "Somalia", GroupName = "AF", Description = "706", ShortName = "SO")]
        SOM = 207,

        [Display(Name = "Suriname", GroupName = "SA", Description = "740", ShortName = "SR")]
        SUR = 208,

        [Display(Name = "Sao Tome and Principe", GroupName = "AF", Description = "678", ShortName = "ST")]
        STP = 209,

        [Display(Name = "El Salvador", GroupName = "NA", Description = "222", ShortName = "SV")]
        SLV = 210,

        [Display(Name = "Sint Maarten", GroupName = "NA", Description = "534", ShortName = "SX")]
        SXM = 211,

        [Display(Name = "Syria", GroupName = "AS", Description = "760", ShortName = "SY")]
        SYR = 212,

        [Display(Name = "Eswatini", GroupName = "AF", Description = "748", ShortName = "SZ")]
        SWZ = 213,

        [Display(Name = "Turks and Caicos Islands", GroupName = "NA", Description = "796", ShortName = "TC")]
        TCA = 214,

        [Display(Name = "Chad", GroupName = "AF", Description = "148", ShortName = "TD")]
        TCD = 215,

        [Display(Name = "French Southern Territories", GroupName = "AN", Description = "260", ShortName = "TF")]
        ATF = 216,

        [Display(Name = "Togo", GroupName = "AF", Description = "768", ShortName = "TG")]
        TGO = 217,

        [Display(Name = "Thailand", GroupName = "AS", Description = "764", ShortName = "TH")]
        THA = 218,

        [Display(Name = "Tajikistan", GroupName = "AS", Description = "762", ShortName = "TJ")]
        TJK = 219,

        [Display(Name = "Tokelau", GroupName = "OC", Description = "772", ShortName = "TK")]
        TKL = 220,

        [Display(Name = "Timor Leste", GroupName = "OC", Description = "626", ShortName = "TL")]
        TLS = 221,

        [Display(Name = "Turkmenistan", GroupName = "AS", Description = "795", ShortName = "TM")]
        TKM = 222,

        [Display(Name = "Tunisia", GroupName = "AF", Description = "788", ShortName = "TN")]
        TUN = 223,

        [Display(Name = "Tonga", GroupName = "OC", Description = "776", ShortName = "TO")]
        TON = 224,

        [Display(Name = "Turkey", GroupName = "AS", Description = "792", ShortName = "TR")]
        TUR = 225,

        [Display(Name = "Trinidad and Tobago", GroupName = "NA", Description = "780", ShortName = "TT")]
        TTO = 226,

        [Display(Name = "Tuvalu", GroupName = "OC", Description = "798", ShortName = "TV")]
        TUV = 227,

        [Display(Name = "Taiwan", GroupName = "AS", Description = "158", ShortName = "TW")]
        TWN = 228,

        [Display(Name = "Tanzania", GroupName = "AF", Description = "834", ShortName = "TZ")]
        TZA = 229,

        [Display(Name = "Ukraine", GroupName = "EU", Description = "804", ShortName = "UA")]
        UKR = 230,

        [Display(Name = "Uganda", GroupName = "AF", Description = "800", ShortName = "UG")]
        UGA = 231,

        [Display(Name = "United States Minor Outlying Islands", GroupName = "OC", Description = "581", ShortName = "UM")]
        UMI = 232,

        [Display(Name = "United States", GroupName = "NA", Description = "840", ShortName = "US")]
        USA = 233,

        [Display(Name = "Uruguay", GroupName = "SA", Description = "858", ShortName = "UY")]
        URY = 234,

        [Display(Name = "Uzbekistan", GroupName = "AS", Description = "860", ShortName = "UZ")]
        UZB = 235,

        [Display(Name = "Vatican", GroupName = "EU", Description = "336", ShortName = "VA")]
        VAT = 236,

        [Display(Name = "Saint Vincent and the Grenadines", GroupName = "NA", Description = "670", ShortName = "VC")]
        VCT = 237,

        [Display(Name = "Venezuela", GroupName = "SA", Description = "862", ShortName = "VE")]
        VEN = 238,

        [Display(Name = "British Virgin Islands", GroupName = "NA", Description = "092", ShortName = "VG")]
        VGB = 239,

        [Display(Name = "U.S. Virgin Islands", GroupName = "NA", Description = "850", ShortName = "VI")]
        VIR = 240,

        [Display(Name = "Vietnam", GroupName = "AS", Description = "704", ShortName = "VN")]
        VNM = 241,

        [Display(Name = "Vanuatu", GroupName = "OC", Description = "548", ShortName = "VU")]
        VUT = 242,

        [Display(Name = "Wallis and Futuna", GroupName = "OC", Description = "876", ShortName = "WF")]
        WLF = 243,

        [Display(Name = "Samoa", GroupName = "OC", Description = "882", ShortName = "WS")]
        WSM = 244,

        [Display(Name = "Yemen", GroupName = "AS", Description = "887", ShortName = "YE")]
        YEM = 245,

        [Display(Name = "Mayotte", GroupName = "AF", Description = "175", ShortName = "YT")]
        MYT = 246,

        [Display(Name = "South Africa", GroupName = "AF", Description = "710", ShortName = "ZA")]
        ZAF = 247,

        [Display(Name = "Zambia", GroupName = "AF", Description = "894", ShortName = "ZM")]
        ZMB = 248,

        [Display(Name = "Zimbabwe", GroupName = "AF", Description = "716", ShortName = "ZW")]
        ZWE = 249,

        [Display(Name = "Serbia and Montenegro", GroupName = "EU", Description = "891", ShortName = "CS")]
        SCG = 250,

        [Display(Name = "Netherlands Antilles", GroupName = "NA", Description = "530", ShortName = "AN")]
        ANT = 251,
    }
    
    public static class CountryExtensions
    {
        public static Dictionary<Country, DisplayAttribute> _countryMap;

        static CountryExtensions()
        {
            _countryMap = new Dictionary<Country, DisplayAttribute>();
            var type = typeof(Country);

            foreach (var country in Enum.GetValues<Country>())
            {
                var memInfo     = type.GetMember(country.ToString());
                var attributes  = memInfo[0].GetCustomAttributes(typeof(DisplayAttribute), false);
                _countryMap[country] = (DisplayAttribute) attributes[0];
            }
        }

        public static DisplayAttribute GetDisplayAttribute(this Country country) => _countryMap[country];
    }
}