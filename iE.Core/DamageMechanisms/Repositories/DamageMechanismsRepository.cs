using iE.Core.DamageMechanisms.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace iE.Core.DamageMechanisms.Repositories
{
    public static class DamageMechanismRepository
    {

        private static readonly Dictionary<string, DamageMechanism> _mechanisms = new Dictionary<string, DamageMechanism>(StringComparer.OrdinalIgnoreCase);

        private static readonly HashSet<string> _materialCategories = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "Carbon Steel",
            "Carbon Steel (non-PWHT)",
            "Carbon Steel (HIC-resistant)",
            "Low-Alloy Steels (Cr-Mo)",
            "300 Series Stainless Steel",
            "400 Series Stainless Steel",
            "Duplex Stainless Steel",
            "Nickel Alloys",
            "Copper Alloys",
            "Titanium",
            "Cast Iron",
            "Refractories/Ceramics",
            "Other"
        };
        static DamageMechanismRepository()
        {
            Console.WriteLine("Static constructor executing...");
            Debug.WriteLine("DamageMechanismRepository static initialization started");
            InitializeMechanisms();
            Debug.WriteLine($"Initialized with {_mechanisms.Count} mechanisms");
        }

        private static void InitializeMechanisms()
        {
            _mechanisms.Clear();

            AddMechanism(new DamageMechanism
            {
                Name = "885°F (475°C) Embrittlement",
                Description = "Loss of ductility and fracture toughness in stainless steels due to formation of alpha prime phase from exposure to 600°F–1000°F (315°C–540°C). Can lead to cracking failures, especially during shutdowns or maintenance.",
                AffectedMaterials = "400 series SS (405, 409, 410, 410S, 430, 446), duplex SS (2205, 2304, 2507), 300 series SS weld metals with ferrite phase.",
                AffectedUnits = "Fractionator trays, internals in crude/vacuum/FCC/coker units, duplex SS heat exchanger tubes.",
                CriticalFactors = "Chromium content, ferrite phase amount, time at 600°F–1000°F (315°C–540°C). Higher Cr alloys (e.g., 430, 446) are more susceptible. Damage is cumulative and worsens at lower temps (e.g., shutdowns).",
                Prevention = "Avoid susceptible materials in critical temp range; use rapid cooling post-welding; de-embrittle via heat treatment (>1100°F/595°C) if practical.",
                Inspection = "Bend/impact testing (most effective), hardness testing (limited), visual crack detection during maintenance. Metallography is ineffective.",
                RiskLevel = "High (for pressure-boundary components); moderate for non-critical parts like trays.",
                RelatedMechanisms = new List<string> { "Sigma Phase Embrittlement", "Temper Embrittlement", "Hydrogen Embrittlement" }
            });



            AddMechanism(new DamageMechanism
            {
                Name = "Amine Corrosion",
                Description = "Localized corrosion of carbon steel in amine treating processes caused by dissolved acid gases (CO₂/H₂S), heat stable amine salts (HSAS), and degradation products (e.g. bicine, oxalate). Not caused by amine itself but contaminants. Weld areas often preferentially attacked.",
                AffectedMaterials = "Primarily carbon steel; 300 series SS more resistant.",
                AffectedUnits = "Amine units (crude/coker/FCC/hydroprocessing), regenerator reboilers, lean/rich exchangers, absorber systems, stripper overhead condensers.",
                CriticalFactors = "Amine type (MEA > DGA > DIPA > DEA > MDEA in aggressiveness), HSAS >2%, oxygen ingress, temperature >220°F (105°C), velocity (3–6 fps for rich amine), overstripping (loss of protective H₂S film).",
                Prevention = "Control acid gas loading, avoid oxygen ingress, filter solids/hydrocarbons, inert gas blanketing, upgrade to 300 series SS in flashing zones, monitor HSAS levels.",
                Inspection = "VT for flow impingement/weld attack, UT thickness mapping, profile RT for welds, corrosion coupons/probes, amine solution iron content monitoring.",
                RiskLevel = "High (severe localized thinning in critical components like reboilers).",
                RelatedMechanisms = new List<string> { "Amine Stress Corrosion Cracking", "Wet H₂S Damage (Blistering/HIC/SOHIC/SSC)", "CO₂ Corrosion" }
            });



            AddMechanism(new DamageMechanism
            {
                Name = "Amine Stress Corrosion Cracking (Amine SCC)",
                Description = "Intergranular cracking of carbon steel under tensile stress in aqueous alkanolamine solutions (e.g., MEA, DEA). A form of alkaline SCC (ASCC), typically initiating at non-PWHT'd welds or cold-worked areas. Cracks are often oxide-filled and parallel to welds.",
                AffectedMaterials = "Carbon steel and low-alloy steels; 300 series SS resistant.",
                AffectedUnits = "Lean amine systems (absorbers, strippers, regenerators), reboilers, piping; also equipment with amine carryover.",
                CriticalFactors = "Residual welding/cold-work stresses (critical), amine type (MEA/DEA > MDEA/DIPA), temperature (risk down to ambient for MEA), lean amine > rich amine (H₂S in rich amine inhibits cracking).",
                Prevention = "PWHT (1175±25°F/635±15°C) per API 945/NACE SP0472, avoid cold work, water wash before steam-out, consider clad/300 series SS for critical zones.",
                Inspection = "WFMT/ACFM for surface cracks, SWUT/PAUT for subsurface, PT (limited for tight cracks), RT ineffective, AET for monitoring growth.",
                RiskLevel = "High (catastrophic failure risk in non-PWHT'd welds).",
                RelatedMechanisms = new List<string> { "Caustic SCC", "Carbonate Stress Corrosion Cracking (ACSCC)", "Ammonia Stress Corrosion Cracking", "Wet H₂S Damage (Blistering/HIC/SOHIC/SSC)" }
            });



            AddMechanism(new DamageMechanism
            {
                Name = "Ammonia Stress Corrosion Cracking",
                Description = "Cracking of susceptible materials caused by exposure to ammonia-containing environments under tensile stress. Occurs in both aqueous ammonia (copper alloys) and anhydrous ammonia (carbon steel) services.",
                AffectedMaterials = "Copper-zinc alloys (especially >15% zinc), carbon steel (in anhydrous ammonia), non-stress-relieved high-strength steels",
                AffectedUnits = "Copper alloy heat exchanger tubes, ammonia storage tanks, refrigeration units, lube oil refining equipment",
                CriticalFactors = "For copper alloys: residual stress, pH >8.5, oxygen presence, water phase with ammonia. For steel: anhydrous ammonia with <0.2% water, oxygen contamination, high residual stresses",
                Prevention = "For copper: Use <15% zinc alloys or cupronickels; prevent air ingress. For steel: Add >0.2% water; stress relief welds; use low-strength steels (<70 ksi); maintain oxygen <1ppm",
                Inspection = "For copper: ECT, VT, PT on rolled areas. For steel: WFMT on welds/HAZs, angle beam UT, AET for crack monitoring",
                RiskLevel = "High (for non-mitigated cases)",
                RelatedMechanisms = new List<string> { "Chloride Stress Corrosion Cracking", "Caustic Stress Corrosion Cracking" }
            });



            AddMechanism(new DamageMechanism
            {
                Name = "Ammonium Bisulfide Corrosion (Alkaline Sour Water)",
                Description = "Aggressive corrosion occurring in hydroprocessing reactor effluent streams and units handling alkaline sour water, especially in high turbulence areas. Caused by ammonium bisulfide (NH4HS) salts.",
                AffectedMaterials = "Carbon steel, low-alloy steels, 300 series SS, duplex stainless steel, nickel-based alloys, titanium (copper alloys are rapidly corroded)",
                AffectedUnits = "Hydroprocessing reactor effluent systems, FCC units, sour water strippers, coker fractionator overheads, amine unit regenerators",
                CriticalFactors = "NH4HS concentration >2%, velocity >20 fps (6 m/s), presence of cyanides, oxygen contamination in wash water, pH and temperature effects",
                Prevention = "Upgrade to duplex SS or nickel alloys for high-velocity areas; ensure proper wash water injection with low oxygen; maintain NH4HS concentration below 2%; use hydraulically balanced flow designs",
                Inspection = "UT thickness monitoring in turbulent zones; guided wave testing (GWT); corrosion coupons; water chemistry analysis; IRIS/MFL for exchanger tubes",
                RiskLevel = "High",
                RelatedMechanisms = new List<string> { "Wet H₂S Damage (Blistering/HIC/SOHIC/SSC)", "Amine Corrosion" }
            });


            AddMechanism(new DamageMechanism
            {
                Name = "Ammonium Chloride and Amine Hydrochloride Corrosion",
                Description = "Localized corrosion, often pitting, occurring under ammonium chloride or amine salt deposits, typically in the absence of free water phase. Salts are hygroscopic and form acidic solutions when hydrated.",
                AffectedMaterials = "Carbon steel, low-alloy steels (most susceptible); 300 series SS; duplex SS; Alloys 400, 800, 825; Alloys 625/C276; titanium (most resistant but can still pit)",
                AffectedUnits = "Crude tower overheads, hydroprocessing reactor effluents, catalytic reformer H₂ recycle systems, FCC/coker fractionator overheads",
                CriticalFactors = "NH₃/HCl/amine salt concentrations; temperatures up to 400°F (205°C); small water amounts trigger aggressive corrosion (>100 mpy); increasing temperature accelerates rates",
                Prevention = "Crude units: Improve desalting, inject wash water, use filming amines. Hydroprocessing: Limit feed chlorides,                use chloride traps.FCC / cokers: Implement continuous water wash",
                Inspection = "RT/UT scanning (AUT/close-grid); GWT screening; monitor water injection nozzles; IRIS/MFL for exchanger tubes; track pressure drops for fouling",
                RiskLevel = "High (Extremely localized with rapid metal loss)",
                RelatedMechanisms = new List<string> { "Hydrochloric Acid Corrosion", "Chloride Stress Corrosion Cracking (Cl-SCC)", "Aqueous Organic Acid Corrosion", "Concentration Cell Corrosion" }
            });



            AddMechanism(new DamageMechanism
            {
                Name = "Aqueous Organic Acid Corrosion",
                Description = "Corrosion caused by low molecular weight organic acids (formic, acetic, propionic) that form from crude oil decomposition and dissolve in water phases, reducing pH and increasing corrosion risk",
                AffectedMaterials = "Carbon steel (most susceptible), low-alloy steels; 300 series SS generally resistant but vulnerable if halogens present",
                AffectedUnits = "Crude/vacuum tower overheads, visbreaker/coker fractionator systems, heat exchangers, separator drums, mix points of recovered oil streams",
                CriticalFactors = "Type/quantity of acids (formic/acetic most corrosive), temperature, velocity, system pH, presence of other acids (HCl/H2S); sudden acid increases cause unexpected pH drops",
                Prevention = "Neutralizer injection adjusted for crude blends; filming amines (less effective); upgrade to corrosion-resistant alloys accounting for other mechanisms; monitor TAN of crudes",
                Inspection = "UT/RT thickness assessment; GWT/EMAT screening; corrosion probes/coupons; infrared thermography for water accumulation; pH monitoring in overhead drums",
                RiskLevel = "Medium (typically less severe than inorganic acids but can be high with certain crudes)",
                RelatedMechanisms = new List<string> { "Hydrochloric Acid (HCl) Corrosion", "Naphthenic Acid Corrosion", "Carbonic Acid Corrosion" }
            });



            AddMechanism(new DamageMechanism
            {
                Name = "Atmospheric Corrosion",
                Description = "Corrosion of exposed metal surfaces due to environmental exposure to moisture, oxygen, and atmospheric contaminants, with severity varying by location and climate conditions",
                AffectedMaterials = "Carbon steel (most vulnerable), low-alloy steels, copper-alloyed aluminum; deterioration accelerates with damaged coatings",
                AffectedUnits = "Unpainted/uninsulated piping/equipment (<250°F/120°C), tank exteriors, pipe supports, piers/docks, structural steel, downwind equipment from cooling towers",
                CriticalFactors = "Marine/industrial environments (20 mpy); humidity/rainfall; chloride/H2S/fly ash contamination; water-trapping designs; temperature cycling; bird droppings",
                Prevention = "High-quality coatings with proper surface prep; cathodic protection for submerged areas; drainage improvements; corrosion-resistant alloys in severe environments",
                Inspection = "VT for coating integrity/rust; UT thickness checks in trap areas; laser scanning/white light imaging for pitting; PEC screening",
                RiskLevel = "Low-Moderate (1-20 mpy based on environment)",
                RelatedMechanisms = new List<string> { "Corrosion Under Insulation (CUI)", "Concentration Cell Corrosion", "Galvanic Corrosion" }
            });



            AddMechanism(new DamageMechanism
            {
                Name = "Boiler Water and Steam Condensate Corrosion",
                Description = "General corrosion and pitting caused by dissolved oxygen/CO₂ in boiler/condensate systems, including flow-accelerated corrosion (FAC) at high-velocity zones",
                AffectedMaterials = "Carbon steel, low-alloy steels (Cr/Mo/Cu alloys improve FAC resistance)",
                AffectedUnits = "Deaerators, feedwater lines, boiler tubes, economizers, condensate return systems, reboilers",
                CriticalFactors = "Oxygen/CO₂ concentration, pH (9.2–9.6 optimal), temperature (FAC peaks at 300°F), feedwater quality, oxygen scavenger efficacy",
                Prevention = "Mechanical deaeration + chemical scavengers (sulfite/hydrazine); amine inhibitors for CO₂; BFW blowdown; upgrade to Cr-Mo steel for FAC",
                Inspection = "UT/RT for wall thinning; water analysis (pH/O₂/CO₂/iron); vacuum testing for air ingress",
                RiskLevel = "Medium (High if FAC/uncontrolled O₂ present)",
                RelatedMechanisms = new List<string> { "CO₂ Corrosion", "Oxygenated Process Water Corrosion", "Corrosion Fatigue" }
            });



            AddMechanism(new DamageMechanism
            {
                Name = "Brine Corrosion",
                Description = "Localized pitting/crevice corrosion in chloride/saltwater environments, accelerated by oxygen, low pH, and microbiological activity",
                AffectedMaterials = "Carbon steel (severe pitting), stainless steels (sharp chloride pits), copper alloys (sulfide-sensitive), Ni-Cr-Mo alloys (resistant), FRP (immune)",
                AffectedUnits = "Desalters, effluent water systems, seawater/firewater piping, salt driers",
                CriticalFactors = "Chloride concentration, O₂ content, pH, temperature, PREN value (≥40 for SS in seawater), biocides (chlorine >0.5ppm worsens corrosion)",
                Prevention = "Deaeration (sodium metabisulfite); upgrade to Ni-Cr-Mo/FRP; limit chlorine; monitor SRB; epoxy coatings (with void prevention)",
                Inspection = "UT scanning/GWT for pitting; corrosion coupons; O₂ monitoring; RT for thick sections",
                RiskLevel = "High (for carbon steel/stainless steels in aerated brine)",
                RelatedMechanisms = new List<string> { "Chloride Stress Corrosion Cracking (Cl-SCC)", "MIC", "Galvanic Corrosion", "Oxygenated Process Water Corrosion" }
            });



            AddMechanism(new DamageMechanism
            {
                Name = "Brittle Fracture",
                Description = "Sudden catastrophic failure with minimal plastic deformation, typically at low temperatures or high stress concentrations",
                AffectedMaterials = "Carbon/low-alloy steels (especially pre-1987 ASME vessels), 400-series SS, embrittled materials (sigma/temper/475°C embrittlement)",
                AffectedUnits = "Pressure vessels (thick-walled), light hydrocarbon units (alkylation/olefins), storage spheres/bullets",
                CriticalFactors = "Low fracture toughness (Charpy transition temp), flaw size/stress, material thickness (triaxial stress), autorefrigeration events",
                Prevention = "ASME UCS-66 compliance; impact testing; fine-grain steels; lower MAWP for existing vessels; avoid cold hydrotests",
                Inspection = "MT/PT/UT for flaws; engineering eval per API 579-1; monitor high-stress zones",
                RiskLevel = "Critical (catastrophic consequences)",
                RelatedMechanisms = new List<string> { "Temper Embrittlement", "Sigma Phase Embrittlement", "885°F (475°C) Embrittlement" }
            });



            AddMechanism(new DamageMechanism
            {
                Name = "Carbonate Stress Corrosion Cracking (ACSCC)",
                Description = "Intergranular cracking of carbon steel in alkaline water containing carbonate ions and H₂S, occurring under tensile stress at welds/cold-worked areas",
                AffectedMaterials = "Carbon steel, low-alloy steels (welds/HAZs most susceptible)",
                AffectedUnits = "FCC unit overhead condensers, wet gas compressors, SW systems, regenerator shells, pumparound coolers",
                CriticalFactors = "pH 8-10, carbonate >100ppm, low S/N ratio (<5) in feed, residual stress, H₂S presence (disrupts protective films), hydrotreated FCC feeds",
                Prevention = "PWHT (1200-1225°F per WRC 452/552); 300 series SS/Alloy 400 cladding; monitor SW pH/CO₃²⁻",
                Inspection = "WFMT/ACFM after surface prep; angle beam UT (SWUT/PAUT); avoid PT (misses tight cracks); AET for growth monitoring",
                RiskLevel = "High (rapid crack propagation; 23 leaks reported in 7 months per Fig 3-12-2)",
                RelatedMechanisms = new List<string> { "Amine Stress Corrosion Cracking (Amine SCC)", "Caustic SCC", "Wet H₂S Damage (Blistering/HIC/SOHIC/SSC)" }
            });



            AddMechanism(new DamageMechanism
            {
                Name = "Carburization",
                Description = "Carbon absorption into metals at >1100°F (595°C) in low-oxygen, hydrocarbon-rich environments, causing embrittlement and loss of corrosion resistance",
                AffectedMaterials = "Carbon steel (brittle surface), 300/400 SS (Cr depletion), Alloys 600/800, HK/HP cast alloys",
                AffectedUnits = "Fired heater tubes (cokers/reformers), ethylene pyrolysis furnaces, decoking cycles",
                CriticalFactors = "CO/CH₄/Coke exposure; T >1100°F; low O₂/steam; coker furnace tubes lose sulfidation resistance",
                Prevention = "Alumina-forming coatings (alonizing); <10ppm sulfur additives; upgrade to stable oxide-forming alloys",
                Inspection = "Magnetic permeability testing (austenitic alloys become ferromagnetic); replication+hardness (destructive); TOFD for case depth; avoid hammer tests",
                RiskLevel = "High (catastrophic carburization causes crumbling/spalling; Fig 3-13-1 shows 1900°F damage)",
                RelatedMechanisms = new List<string> { "Metal Dusting", "Sulfidation", "Creep and Stress Rupture" }
            });



            AddMechanism(new DamageMechanism
            {
                Name = "Caustic Corrosion (Gouging)",
                Description = "Localized thinning under concentrated NaOH/KOH deposits from DNB/evaporation, or general corrosion in high-strength caustic >170°F (75°C)",
                AffectedMaterials = "Carbon steel (severe), 400SS; 300SS resistant until 160-210°F (70-100°C) passivity loss",
                AffectedUnits = "Boilers/steam generators, crude unit preheat exchangers (caustic injection points), caustic storage tanks",
                CriticalFactors = "Caustic concentration (BFW treatment/demineralizer leaks); heat tracing; chlorides accelerate attack",
                Prevention = "Design to prevent DNB; proper caustic mixing; Alloy 400 for high-strength caustic; control T <170°F",
                Inspection = "UT scanning for grooves under deposits (Fig 3-14-1); boroscope for tubes; monitor injection points per API 570/574",
                RiskLevel = "High (rapid metal loss; Fig 3-14-2 shows deep localized attack)",
                RelatedMechanisms = new List<string> { "Caustic SCC", "Boiler Water and Steam Condensate Corrosion", "Under-Deposit Corrosion" }
            });



            AddMechanism(new DamageMechanism
            {
                Name = "Caustic Stress Corrosion Cracking",
                Description = "Surface-initiated intergranular cracking in alkaline solutions (NaOH/KOH) at elevated temperatures, primarily at non-PWHT'd welds. A form of alkaline SCC (ASCC) with crack growth rates accelerating dramatically during temperature excursions.",
                AffectedMaterials = "Carbon steel (most susceptible), 300 series SS, duplex SS (improved resistance); nickel alloys resistant but not immune above 604°F (318°C)",
                AffectedUnits = "H₂S/mercaptan removal units, alkylation units (HF/sulfuric acid), caustic-injected crude towers, improperly heat-traced piping, boiler tubes/tubesheets (Fig 3-15-4)",
                CriticalFactors = "Caustic concentration (>50ppm if concentrated); T >140°F (60°C); residual welding/cold work stresses; sulfides increase susceptibility even at low temps (area 'A' in Fig 3-15-1)",
                Prevention = "PWHT at 1150°F (620°C) for 1hr; nickel alloy trim for high-T/concentration; avoid steam-outs of non-PWHT'd carbon steel; proper caustic injection mixing",
                Inspection = "WFMT/ACFM after surface prep; angle beam UT (SWUT/PAUT) for crack sizing; PT ineffective for tight oxide-filled cracks; AET for growth monitoring",
                RiskLevel = "High (cracks can penetrate walls in hours during temp excursions)",
                RelatedMechanisms = new List<string> { "Amine SCC", "Carbonate Stress Corrosion Cracking (ACSCC)", "Chloride Stress Corrosion Cracking (Cl-SCC)" }
            });

            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            AddMechanism(new DamageMechanism
            {
                Name = "Cavitation",
                Description = "Localized pitting/gouging from implosion of vapor bubbles in low-pressure zones, causing microjet impacts (Fig 3-16-1). Accelerated in corrosive environments (cavitation-corrosion).",
                AffectedMaterials = "All common metals (CS, cast iron, 300/400SS, nickel alloys); hardness/toughness balance critical",
                AffectedUnits = "Pump impellers/casings (Fig 3-16-4), downstream of control valves (Fig 3-16-2), heat exchanger tubes, venturis",
                CriticalFactors = "Inadequate NPSH; T near liquid boiling point; turbulent flow; abrasive particles accelerate damage",
                Prevention = "Increase suction pressure; streamline flow; hardfacing/ceramic coatings; avoid entrained air; monitor pump efficiency curves",
                Inspection = "VT/boroscopy for pitting (Fig 3-16-3); UT scanning (challenging for small pits); acoustic monitoring for bubble collapse sounds",
                RiskLevel = "Medium (localized damage but can cause rapid failure in pumps)",
                RelatedMechanisms = new List<string> { "Erosion-Corrosion", "Impingement Attack" }
            });



            AddMechanism(new DamageMechanism
            {
                Name = "Chloride Stress Corrosion Cracking (Cl-SCC)",
                Description = "Branched transgranular cracking of 300 series SS (8-12% Ni most susceptible) in aqueous chloride environments >140°F (60°C). Occurs under insulation or in process streams with chloride concentration mechanisms.",
                AffectedMaterials = "300 series SS (welds with ferrite more resistant); duplex SS (improved); nickel alloys >35% Ni nearly immune",
                AffectedUnits = "Insulated SS piping (external, Fig 3-17-1), crude overheads, hydroprocessing drains, bellows, biofeed units (organic chloride conversion)",
                CriticalFactors = "Cl⁻ concentration (no safe threshold); T >140°F; pH 2-12; sensitization causes intergranular cracks; oxygen accelerates",
                Prevention = "Use carbon steel/400SS; dry hydrotest water; chloride-free coatings under insulation; avoid stagnant zones; Ni alloys for severe service",
                Inspection = "PT (polish surface first); ECT for tubes; angle beam UT (limited by crack branching); RT ineffective for fine cracks",
                RiskLevel = "High (catastrophic in pressure equipment; Fig 3-17-7 shows Alloy C-276 failure)",
                RelatedMechanisms = new List<string> { "Caustic SCC", "Polythionic Acid Stress Corrosion Cracking (PTA SCC)", "Brine Corrosion" }
            });



            AddMechanism(new DamageMechanism
            {
                Name = "CO₂ Corrosion",
                Description = "Results when CO₂ dissolves in water to form carbonic acid (H₂CO₃), lowering pH and promoting general or pitting corrosion of carbon steel.",
                AffectedMaterials = "Carbon steel, low-alloy steels. 300 series austenitic SS is highly resistant.",
                AffectedUnits = "BFW and condensate systems, effluent gas streams in hydrogen plants, overhead systems of regenerators in CO₂ removal plants, crude tower overhead systems.",
                CriticalFactors = "Liquid water presence, CO₂ partial pressure, pH, temperature, oxygen contamination, velocity, turbulence, condensation locations.",
                Prevention = "Corrosion inhibitors, increasing condensate pH >6, upgrading to 300 series SS, maintaining insulation, internal coatings.",
                Inspection = "VT, UT, RT (profile RT), remote video probes, angle beam UT for welds, thickness monitoring sensors, water analysis monitoring.",
                RiskLevel = "High (rates up to 1000 mpy observed)",
                RelatedMechanisms = new List<string> { "Boiler Water and Steam Condensate Corrosion", "\"Carbonate Stress Corrosion Cracking (ACSCC)" }
            });



            AddMechanism(new DamageMechanism
            {
                Name = "Concentration Cell Corrosion",
                Description = "Accelerated corrosion due to differences in electrolyte concentration (e.g., under deposits or in crevices), often creating differential aeration cells.",
                AffectedMaterials = "Carbon steel (most susceptible), low-alloy steels, stainless steels. Higher alloys (e.g., Ni-Cr-Mo, titanium) have good resistance.",
                AffectedUnits = "All process/utility piping/equipment (internal or external), locations with deposits, crevices (e.g., flange faces, tray components), pipe supports.",
                CriticalFactors = "Aqueous environment, oxygen concentration differences, chloride content (for SS), stagnant conditions, deposit accumulation.",
                Prevention = "Minimize deposits/crevices, use welded pipe supports, repair peeling coatings, upgrade materials (e.g., higher PREN alloys) where feasible.",
                Inspection = "VT with pit gauging, UT thickness mapping, scanning UT for large surfaces, RFT/ECT for tubes, monitor fouling via pressure drop/thermal performance.",
                RiskLevel = "Medium to High (localized accelerated attack)",
                RelatedMechanisms = new List<string> { "Under-Deposit Corrosion", "Crevice corrosion", "CUI", "Ammonium Chloride and Amine Hydrochloride Corrosion" }
            });



            AddMechanism(new DamageMechanism
            {
                Name = "Cooling Water Corrosion",
                Description = "General/localized corrosion caused by dissolved salts, gases, organics, or microbiological activity in cooling water systems.",
                AffectedMaterials = "Carbon steel, stainless steels, copper alloys, aluminum, titanium, nickel alloys.",
                AffectedUnits = "Cooling towers, piping, pumps, water-cooled heat exchangers.",
                CriticalFactors = "Water type (fresh/brackish/salt), temperature (>140°F scaling risk), oxygen content, velocity (low: <3 fps causes fouling; high: erosion), chloride-induced SCC in SS.",
                Prevention = "Control water chemistry/temperature/velocity, upgrade materials (e.g., for high chloride), mechanical cleaning, sacrificial anodes, tube-side water flow.",
                Inspection = "Monitor water (pH, O₂, chlorides), U-factor calculations, corrosion coupons/ER probes, RFT/ECT/IRIS for tubes, destructive tube analysis.",
                RiskLevel = "Medium (varies with water quality)",
                RelatedMechanisms = new List<string> { "MIC", "Chloride Stress Corrosion Cracking (Cl-SCC)", "Galvanic corrosion" }
            });



            AddMechanism(new DamageMechanism
            {
                Name = "Corrosion Fatigue",
                Description = "Fatigue cracking accelerated by corrosive environments, reducing cycles to failure. Initiates at stress concentrations (e.g., pits, welds).",
                AffectedMaterials = "All metals/alloys. Common in carbon steel, stainless steels.",
                AffectedUnits = "Rotating equipment (pump shafts), deaerators, cycling boilers, components under cyclic stress (mechanical/thermal).",
                CriticalFactors = "Cyclic stress level/frequency, corrosive environment (pitting risk), residual stresses (welds), temperature fluctuations.",
                Prevention = "Material selection, coatings, stress relief (PWHT), smooth weld contours, slow boiler startups, proper water chemistry.",
                Inspection = "VT/UT/PT/MT for rotating equipment; WFMT/ACFM for deaerators; angle beam UT/EMAT for boilers; monitor for pinhole leaks.",
                RiskLevel = "High (no endurance limit in corrosive environments)",
                RelatedMechanisms = new List<string> { "Mechanical fatigue", "Thermal fatigue", "Boiler Water and Steam Condensate Corrosion" }
            });



            AddMechanism(new DamageMechanism
            {
                Name = "Corrosion Under Insulation (CUI)",
                Description = "Corrosion of piping, pressure vessels, and structural components resulting from water trapped under insulation or fireproofing.",
                AffectedMaterials = "Carbon steel, low-alloy steels, 300/400 series SS, duplex stainless steels.",
                AffectedUnits = "All insulated piping/equipment, especially at damaged insulation, flange terminations, pipe supports, low points, vibrating piping, or areas with steam tracing leaks.",
                CriticalFactors = "Temperature (10°F–350°F for carbon steel; 140°F–350°F for 300 series SS), moisture ingress, insulation type (wicking/slow-drying), chloride/SO₂ contamination, cyclic operation, marine environments.",
                Prevention = "High-quality coatings (immersion-resistant or flame-sprayed Al), proper insulation maintenance, thin Al foil for SS, low-chloride insulation for SS, removable/replaceable insulation plugs, alternative designs (e.g., metal standoffs).",
                Inspection = "VT for insulation damage, UT/RT (profile/density), pit gauging, PT for SS SCC, non-invasive methods (GWT, PEC, neutron backscatter, IR thermography). Focus on high-risk areas (cooling tower drift zones, steam vents).",
                RiskLevel = "High (localized leaks/thinning, CI⁻ SCC in SS).",
                RelatedMechanisms = new List<string> { "Atmospheric corrosion", "Chloride Stress Corrosion Cracking (Cl-SCC)", "Concentration cell corrosion" }
            });

            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            AddMechanism(new DamageMechanism
            {
                Name = "Creep and Stress Rupture",
                Description = "Time-dependent deformation and cracking under load at high temperatures (>50% of melting point), progressing from microvoids to fissures and eventual rupture.",
                AffectedMaterials = "All metals/alloys (thresholds: carbon steel >650°F, Cr-Mo steels >800°F, austenitic SS >950°F).",
                AffectedUnits = "Fired heater tubes, boiler tubes, catalytic reformer reactors, FCC internals, DMWs (ferritic-austenitic).",
                CriticalFactors = "Temperature/stress sensitivity (25°F increase ≈ 50% life reduction), coarse-grained structures (better creep strength), low creep ductility in welds/HAZs, corrosion-induced stress increases.",
                Prevention = "Minimize temperatures/stress concentrations, select creep-resistant alloys (e.g., 9Cr-1Mo-V), PWHT optimization, repair by grinding/re-welding (for reactors).",
                Inspection = "FMR (surface-only), VT for bulging/cracks, UT thickness/dimensional checks, metallography for voids/fissures. Early detection is difficult; focus on welds in Cr-Mo alloys.",
                RiskLevel = "Critical (catastrophic rupture possible with minimal warning).",
                RelatedMechanisms = new List<string> { "Short-term overheating", "Stress-relief cracking", "Dissimilar Metal Weld (DMW) Cracking" }
            });



            AddMechanism(new DamageMechanism
            {
                Name = "Dealloying (Selective Leaching)",
                Description = "Preferential removal of alloy constituents (e.g., Zn, Ni, Al), leaving porous, weakened material. Common in copper alloys and Alloy 400.",
                AffectedMaterials = "Brasses (>15% Zn), aluminum bronzes, Cu-Ni alloys, Alloy 400 (denickelification in HF).",
                AffectedUnits = "Cooling water tubes/tubesheets, BFW bronze pumps, Alloy 400 HF alkylation components, steam turbine condensers.",
                CriticalFactors = "Stagnant/low-flow conditions, oxygen presence, temperature, pH. Specific to alloy-environment combinations (e.g., dezincification in brasses with high Zn).",
                Prevention = "Use inhibited alloys (e.g., admiralty brass with As/Sb), heat treatment (for aluminum bronze), alternative materials (non-susceptible alloys), oxygen exclusion in HF service.",
                Inspection = "VT for color changes (reddish Cu in brasses), hardness testing, metallography, acoustic/ECT screening. UT thickness measurements ineffective.",
                RiskLevel = "Medium (sudden failure possible despite minimal visible changes).",
                RelatedMechanisms = new List<string> {"Graphitic Corrosion of Cast Irons",

    }
            });



            AddMechanism(new DamageMechanism
            {
                Name = "Decarburization",
                Description = "Loss of carbon from steel surfaces due to high-temperature exposure, reducing strength. Often associated with HTHA in hydrogen service.",
                AffectedMaterials = "Carbon steels, low-alloy steels (Cr-Mo more resistant).",
                AffectedUnits = "Heat-treated components, fired heater tubes, hydroprocessing/catalytic reformer equipment, hot-formed pressure vessels.",
                CriticalFactors = "Temperature/time exposure, low carbon activity gas phases (e.g., H₂). Shallow decarburization may indicate overheating.",
                Prevention = "Control gas chemistry, use Cr/Mo steels (stable carbides), follow API RP 941 for hydrogen service.",
                Inspection = "Metallography (carbide-free layer), hardness testing (screening only), destructive sampling for confirmation. FMR/VT ineffective.",
                RiskLevel = "Medium (strength reduction; severe cases indicate HTHA risk).",
                RelatedMechanisms = new List<string> {
        "High-temperature hydrogen attack (HTHA)"
    }
            });



            AddMechanism(new DamageMechanism
            {
                Name = "Dissimilar Metal Weld (DMW) Cracking",
                Description = "Cracking in welds joining dissimilar metals due to thermal expansion mismatch, hydrogen embrittlement, or corrosive environments. Four primary types: thermal expansion-induced creep cracking, hard mixed-zone cracking in wet H₂S, carbon steel-Alloy 400 cracking in HF acid, and hydrogen disbonding in high-temperature hydrogen service.",
                AffectedMaterials = "Carbon steel/low-alloy steels welded to austenitic SS (e.g., 300 series) or nickel alloys (e.g., Alloy 400/800).",
                AffectedUnits = "Clad pipe transitions, hydroprocessing reactor outlets, reformer furnace pigtails, HF alkylation units, FCC/coker vessels, superheaters with ferritic-austenitic welds.",
                CriticalFactors = "Thermal expansion mismatch (25–30% difference), temperatures >500°F (260°C), wet H₂S (hard mixed zones), HF acid exposure, hydrogen partial pressure, poor weld geometry, carbon migration (soft zones in HAZs above 800°F/425°C).",
                Prevention = "Use nickel-based filler metals (lower thermal stress), buttering + PWHT, avoid socket welds >250°F, relocate welds to low-temperature zones, flanged connections in HF service, follow API 577/582 for welding procedures.",
                Inspection = "PT/UT (SWUT/PAUT) during fabrication; RT/WFMT for magnetic materials; FMR/advanced UT for creep damage; PMI for material verification. Challenging due to beam refraction in austenitic welds.",
                RiskLevel = "High (catastrophic failures possible, especially in high-temperature/hydrogen service).",
                RelatedMechanisms = new List<string> {        "Thermal fatigue",        "Wet H₂S Damage (Blistering/HIC/SOHIC/SSC)",        "Hydrogen Stress Cracking in Hydrofluoric Acid"
    }
            });



            AddMechanism(new DamageMechanism
            {
                Name = "Erosion/Erosion-Corrosion",
                Description = "Material loss from mechanical abrasion by solids/liquids/vapors or velocity-assisted corrosion where protective films are removed. Dominated by corrosion in refining environments.",
                AffectedMaterials = "Carbon steel, copper alloys, refractories; materials lacking passivity (reliant on protective scales).",
                AffectedUnits = "Piping elbows/tees, catalyst handling equipment, hydroprocessing reactor effluent lines, coker heaters, pump impellers, boiler tubes (FAC).",
                CriticalFactors = "Particle velocity/hardness, flow turbulence, angle of impact, corrosivity (e.g., ammonium bisulfide, naphthenic acids). Threshold velocities exist (e.g., 4 fps for admiralty brass in seawater).",
                Prevention = "Increase pipe diameter (reduce velocity), streamline bends, hardfacing, corrosion-resistant alloys (e.g., 316 SS), impingement plates/ferrules, inhibitors for specific corrosives (e.g., deaeration for FAC).",
                Inspection = "UT grids/scans for localized thinning, profile RT (screening), GWT/SLOFEC for bottom-of-line gouging, IR thermography for refractory erosion, process stream solids analysis.",
                RiskLevel = "Medium-High (rapid failure possible in high-velocity/corrosive services).",
                RelatedMechanisms = new List<string> {
        "Cavitation",
        "Flow-accelerated corrosion (FAC)"
    }
            });



            AddMechanism(new DamageMechanism
            {
                Name = "Ethanol Stress Corrosion Cracking (Ethanol SCC)",
                Description = "Surface-initiated intergranular cracks in carbon steel caused by combined tensile stress and exposure to fuel-grade ethanol (FGE) or FGE/gasoline blends. Dissolved oxygen and cyclic stresses accelerate cracking.",
                AffectedMaterials = "All grades of carbon steel (primary). Non-metallics (seals, coatings) may degrade but do not crack.",
                AffectedUnits = "Carbon steel storage tanks, rack piping, transfer lines. Not reported in manufacturing equipment or <20% FGE blends.",
                CriticalFactors = "Tensile stress (applied/residual), dissolved oxygen, water content (0.1–4.5 vol%), chloride contamination (>20 ppm shifts cracking to transgranular), galvanic coupling to corroded steel.",
                Prevention = "PWHT (where possible), internal coatings, minimize cold work/lap welds, avoid stress concentrators in design.",
                Inspection = "WFMT (preferred for tight, branched cracks), angle beam UT (SWUT/PAUT) for internal cracks, ACFM (less prep than WFMT). Leaks often precede visual detection.",
                RiskLevel = "High (sudden failures possible; cracks propagate undetected until leakage).",
                RelatedMechanisms = new List<string> {        "Amine Stress Corrosion Cracking",        "Carbonate Stress Corrosion Cracking (ACSCC)",        "Caustic Stress Corrosion Cracking"
    }
            });



            AddMechanism(new DamageMechanism
            {
                Name = "Flue Gas Dew Point Corrosion",
                Description = "Acidic corrosion from condensation of sulfur/nitrogen/chlorine species (e.g., H₂SO₄, HCl) in flue gas when metal temperatures fall below dew points (e.g., 280°F/140°C for H₂SO₄).",
                AffectedMaterials = "Carbon steel, low-alloy steels, 300 series SS (risk of chloride SCC in HRSGs).",
                AffectedUnits = "Economizers/stacks in SRUs, FCC regenerators, fired heaters/boilers burning sulfurous fuels, HRSG feedwater heaters (SS).",
                CriticalFactors = "Fuel contaminants (S/N/Cl), metal temperature below dew point, water washing without neutralization (acidic ash residues).",
                Prevention = "Maintain surface temps >10°F above H₂SO₄ dew point, avoid 300 series SS in chloride-containing HRSGs, add sodium carbonate to final rinse water.",
                Inspection = "UT thickness measurements (fin removal often required), PT/ECT for SS SCC, VT/remote cameras for stack interiors.",
                RiskLevel = "Medium (general wastage; localized pitting or SCC in SS).",
                RelatedMechanisms = new List<string> {        "Chloride Stress Corrosion Cracking (Cl-SCC)",        "Sulfuric acid corrosion"
    }
            });



            AddMechanism(new DamageMechanism
            {
                Name = "Fuel Ash Corrosion",
                Description = "Accelerated high-temperature wastage due to molten salt deposits formed from contaminants (e.g., sulfur, sodium, vanadium) in fuel oil or coal.",
                AffectedMaterials = "All conventional heater/boiler alloys; 50Cr-50Ni alloys show improved resistance.",
                AffectedUnits = "Fired heaters, boilers, gas turbines (especially tube hangers/supports, blades).",
                CriticalFactors = "Contaminant concentration (Na + V >50 ppm), metal temperature (>slag melting point), alloy composition, reducing vs. oxidizing conditions.",
                Prevention = "Blend/change fuel sources, operate below slag melting temps, use 50Cr-50Ni alloys, inject additives to modify slag properties.",
                Inspection = "VT (slag visible), UT thickness gauging, grit blasting to remove deposits.",
                RiskLevel = "High (rates up to 1000 mpy possible)",
                RelatedMechanisms = new List<string>
                {

                }
            });



            AddMechanism(new DamageMechanism
            {
                Name = "Galvanic Corrosion",
                Description = "Accelerated corrosion at junctions of dissimilar metals in conductive fluids (e.g., moist environments, soils).",
                AffectedMaterials = "All metals except noble metals; severity depends on electrochemical potential difference.",
                AffectedUnits = "Heat exchangers (tubes/tubesheet mismatch), buried piping, ship hulls.",
                CriticalFactors = "Metal potential difference, electrolyte conductivity, anode-to-cathode area ratio (small anode = high corrosion).",
                Prevention = "Avoid dissimilar metal coupling, coat both metals, use isolation flanges, install sacrificial anodes.",
                Inspection = "VT (oxidized anode), UT thickness gauging, hidden damage under bolts/rivets.",
                RiskLevel = "Medium (depends on environment/design)",
                RelatedMechanisms = new List<string> {
        "Concentration cell corrosion",
        "Soil corrosion"
    }
            });



            AddMechanism(new DamageMechanism
            {
                Name = "Gaseous Oxygen-enhanced Ignition",
                Description = "Spontaneous combustion of metals/nonmetals in oxygen-rich (>25% O₂) environments due to pressure/temperature/contaminants.",
                AffectedMaterials = "Carbon steel (flammable >15 psig O₂), 300SS (resistant <200 psig), Cu/Ni alloys (best resistance), Ti (avoided).",
                AffectedUnits = "Oxygen piping (valves/regulators), SRUs, FCC units, gasification systems.",
                CriticalFactors = "O₂ pressure (>500 psig = severe), velocity (particle impingement), cleanliness (hydrocarbon contamination), component geometry.",
                Prevention = "Maintain cleanliness, limit velocity (<100 fps), avoid sudden pressure changes, use O₂-compatible materials/lubricants.",
                Inspection = "VT (heat damage, valve malfunctions), blacklight for hydrocarbon contamination.",
                RiskLevel = "Critical (catastrophic ignition risk)",
                RelatedMechanisms = new List<string> {
                "Metal Dusting"}
            });



            AddMechanism(new DamageMechanism
            {
                Name = "Graphitic Corrosion of Cast Irons",
                Description = "Dealloying where iron matrix corrodes, leaving porous graphite. Occurs in low-pH/stagnant conditions (e.g., soils, sulfates).",
                AffectedMaterials = "Gray/nodular/malleable cast irons; white iron immune (no free graphite).",
                AffectedUnits = "Underground piping, feedwater pumps/valves, fire-water systems.",
                CriticalFactors = "pH, temperature (<200°F), aeration, exposure time, sulfate concentration.",
                Prevention = "Coatings/cement linings (internal), cathodic protection (external), substitute materials.",
                Inspection = "Field hardness testing, UT (interface detection), metallography (confirm extent).",
                RiskLevel = "Low-Medium (slow progression)",
                RelatedMechanisms = new List<string> {        "Dealloying"
    }
            });



            AddMechanism(new DamageMechanism
            {
                Name = "Graphitization",
                Description = "Decomposition of carbides into graphite in carbon/C-½Mo steels after long-term exposure to 800–1100°F (425–595°C).",
                AffectedMaterials = "Carbon steel, C-1/2Mo steel (Cr-containing alloys resistant).",
                AffectedUnits = "FCC/coker piping, reformer reactors, boiler economizers.",
                CriticalFactors = "Temperature/time, stress, steel composition (Cr inhibits), weld HAZ susceptibility.",
                Prevention = "Use Cr-containing alloys for >800°F service.",
                Inspection = "Metallography (full-thickness samples), advanced stages may show creep cracks.",
                RiskLevel = "Low (rare, but severe if eyebrow graphitization occurs)",
                RelatedMechanisms = new List<string> {        "Spheroidization"
    }
            });



            AddMechanism(new DamageMechanism
            {
                Name = "High-temperature H₂/H₂S Corrosion",
                Description = "Severe sulfidation corrosion in H₂/H₂S-containing hydrocarbon streams above 450°F (230°C), leading to uniform thinning and rupture-type failures.",
                AffectedMaterials = "Carbon steel, low-alloy steels, 400 series SS, 300 series SS. Minimum 9Cr-1Mo required for practical improvement over carbon steel.",
                AffectedUnits = "Hydroprocessing units (hydrotreaters, hydrocrackers), piping, and equipment exposed to high-temperature H₂/H₂S streams.",
                CriticalFactors = "Temperature (>450°F), H₂ presence, H₂S concentration/partial pressure, vapor/liquid ratio, alloy composition (Cr content).",
                Prevention = "Use high-Cr alloys (e.g., 300 series SS), aluminum diffusion treatment for thin components, monitor H₂S levels via process simulations.",
                Inspection = "UT/RT for thinning, VT for internal scaling, tube-skin thermocouples/infrared thermography for temperature verification.",
                RiskLevel = "High (rupture risk due to uniform thinning)",
                RelatedMechanisms = new List<string> {        "Sulfidation",        "Decarburization"
    }
            });



            AddMechanism(new DamageMechanism
            {
                Name = "High-temperature Hydrogen Attack (HTHA)",
                Description = "Formation of CH₄ from H₂ reacting with carbon in steel at elevated temps/pressures, causing decarburization, internal fissures, and cracking.",
                AffectedMaterials = "Carbon steel (welded/non-PWHT most susceptible), low-alloy steels (Cr/Mo improve resistance), 300/400 series SS immune.",
                AffectedUnits = "Hydroprocessing units, catalytic reformers, hydrogen plants, high-pressure steam boiler tubes.",
                CriticalFactors = "Temperature/H₂ partial pressure (per API RP 941 curves), time, stress levels, alloy composition (Cr/Mo stabilize carbides).",
                Prevention = "Use Cr/Mo alloys with safety margins (25–50°F/15–30°C below API RP 941 curves), avoid C-½Mo steel, apply SS cladding.",
                Inspection = "Specialized NDE (AUBT, ABSA, TOFD, PAUT) for fissures; metallography/SEM for early-stage damage; monitor operating conditions.",
                RiskLevel = "Critical (irreversible damage, incubation period)",
                RelatedMechanisms = new List<string> {        "Decarburization",        "Creep"
    }
            });



            AddMechanism(new DamageMechanism
            {
                Name = "Hydrochloric Acid (HCl) Corrosion",
                Description = "General/localized corrosion from aqueous HCl, often due to dew point condensation in overhead systems or under salt deposits.",
                AffectedMaterials = "All common materials (aggressive to carbon steel, SS). Alloy 400/titanium resist dilute HCl.",
                AffectedUnits = "Crude/vacuum units (overhead systems), hydroprocessing units (effluent trains), catalytic reformers.",
                CriticalFactors = "HCl concentration, temperature, presence of oxidizing agents (O₂, Fe³⁺), water condensation, salt deposits (NH₄Cl).",
                Prevention = "Crude desalting, caustic/amine injection, water washing, upgrade to Ni alloys/titanium, monitor chloride/pH in overhead water.",
                Inspection = "UT thickness mapping, RT for dead-legs, corrosion coupons/probes, VT for orange-yellow discoloration/scale.",
                RiskLevel = "High (rapid thinning in dew point zones)",
                RelatedMechanisms = new List<string> {        "Ammonium Chloride and Amine Hydrochloride Corrosion","Chloride Stress Corrosion Cracking (Cl-SCC)","Aqueous organic acid corrosion"
    }
            });



            AddMechanism(new DamageMechanism
            {
                Name = "Hydrofluoric Acid (HF) Corrosion",
                Description = "General/localized corrosion in HF alkylation units, exacerbated by water content, phase changes, and residual elements (RE) in carbon steel.",
                AffectedMaterials = "Carbon steel (RE-sensitive), Alloy 400 (hot service). Low-alloy/SS unsuitable.",
                AffectedUnits = "HF alkylation units (reactor circuits, fractionation, acid relief systems), flare piping.",
                CriticalFactors = "HF concentration (water content), temperature, RE content (%C/Cu/Ni/Cr), oxygen contamination, dead-legs.",
                Prevention = "Control water (<1.5 wt%), minimize acid carryover, PWHT carbon steel, use Alloy 400 in hot service, monitor RE content per API 751.",
                Inspection = "UT/AUT for localized thinning, profile RT for small-bore piping, PAUT for flange faces, monitor RE content.",
                RiskLevel = "Critical (localized/phase-change corrosion)",
                RelatedMechanisms = new List<string> {        "Hydrogen Stress Cracking in Hydrofluoric Acid",        "HIC/SOHIC",        "Hydrofluoric Acid Stress Corrosion Cracking (HF SCC)"
    }
            });



            AddMechanism(new DamageMechanism
            {
                Name = "Hydrofluoric Acid Stress Corrosion Cracking (HF SCC)",
                Description = "Cracking of nickel alloys (Alloy 400/K-500) in HF vapor due to CuF₂ formation from oxygen contamination and tensile stress.",
                AffectedMaterials = "Alloy 400, Alloy K-500 (high-strength), Ni-Cr-Mo alloys (C-276, 625).",
                AffectedUnits = "HF alkylation units (pressure boundaries, internals, valves, instruments).",
                CriticalFactors = "Oxygen ingress, residual/cold-work stresses, hardness (HRC >30 in K-500), CuF₂ deposits (green crystals).",
                Prevention = "Stress relief (1300°F/705°C for Alloy 400), limit K-500 hardness, avoid Alloy 400 bolting, control purge gas O₂ (<100 ppm).",
                Inspection = "PT for cracks on green-crystal surfaces, monitor purge gas O₂ levels.",
                RiskLevel = "High (catastrophic if undetected)",
                RelatedMechanisms = new List<string> {        "Liquid Metal Embrittlement (LME)",        "Dealloying (Selective Leaching)"
    }
            });



            AddMechanism(new DamageMechanism
            {
                Name = "Hydrogen Embrittlement (HE)",
                Description = "Loss of strength, ductility, or fracture toughness due to atomic hydrogen penetration and diffusion, leading to brittle cracking. Occurs during manufacturing, welding, or service.",
                AffectedMaterials = "Low-alloy steels, high-strength steels, 400 series SS, precipitation hardenable stainless steel, duplex stainless steel, high-strength nickel-based alloys. Carbon steel if hardened (>HRC 22).",
                AffectedUnits = "Cr-Mo reactors/drums, storage spheres, high-strength bolts/springs, hydroprocessing/catalytic reforming units, wet H2S/HF acid services.",
                CriticalFactors = "Material susceptibility, critical hydrogen concentration, high hardness (>HRC 22), residual/applied stress, ambient temperature range (most severe). Hydrogen sources: welding, corrosion, high-temperature H2 gas, pickling, cathodic protection.",
                Prevention = "Control hardness/PWHT, use dry/low-hydrogen electrodes, bake-out (400°F+), avoid cadmium plating, protective linings/cladding.",
                Inspection = "PT/MT/WFMT for surface cracks; angle beam UT (SWUT/PAUT); hydrogen flux monitoring in aqueous environments.",
                RiskLevel = "High (sudden brittle failure)",
                RelatedMechanisms = new List<string> {        "Wet H₂S Damage (Blistering/HIC/SOHIC/SSC)",        "Hydrogen Stress Cracking in Hydrofluoric Acid",        "Wet H₂S Damage (Blistering/HIC/SOHIC/SSC)"
    }
            });



            AddMechanism(new DamageMechanism
            {
                Name = "Hydrogen Stress Cracking in Hydrofluoric Acid",
                Description = "Environmental cracking in high-hardness zones (welds/HAZ) due to hydrogen charged by HF acid corrosion.",
                AffectedMaterials = "Carbon steel, low-alloy steels (hardness >HRC 22).",
                AffectedUnits = "HF acid-exposed piping/equipment, ASTM A193-B7/B7M bolts, valve/compressor parts.",
                CriticalFactors = "Hardness (>HRC 22), tensile stress (welding/cold-forming), rapid crack initiation (hours to delayed).",
                Prevention = "Weld procedure controls (CE/RE chemistry), PWHT, ASTM A193-B7M bolts, alloy cladding/coatings.",
                Inspection = "PT/MT/WFMT for cracks; angle beam UT; hardness testing (weld/bolts).",
                RiskLevel = "High (rapid failure in HF service)",
                RelatedMechanisms = new List<string> {        "Hydrogen Embrittlement",        "Wet H₂S Damage (Blistering/HIC/SOHIC/SSC)",

    }
            });



            AddMechanism(new DamageMechanism
            {
                Name = "Liquid Metal Embrittlement (LME)",
                Description = "Sudden brittle cracking when molten/low-melting metals (e.g., Zn, Hg) contact susceptible alloys. Occurs during fires or mercury contamination.",
                AffectedMaterials = "Carbon steel (Pb/Cd), 300 series SS (Zn/Cu), Alloy 400 (Hg), aluminum (Hg/Sn), titanium (Hg).",
                AffectedUnits = "Post-fire equipment, mercury-contaminated streams (crude overheads, LNG), galvanized-SS contact zones.",
                CriticalFactors = "Specific metal-embrittler pairs, tensile stress, tiny contaminant amounts, temperature >embrittler melting point.",
                Prevention = "Avoid contact (e.g., no galvanized-SS welding); remove Hg from crude; protect SS from Zn overspray.",
                Inspection = "VT for cracks; PT (nonmagnetic)/MT (ferritic); RT/boroscope for Hg deposits.",
                RiskLevel = "High (catastrophic brittle failure)",
                RelatedMechanisms = new List<string> {        "Solid Metal Embrittlement",
    }
            });



            AddMechanism(new DamageMechanism
            {
                Name = "Mechanical Fatigue (Vibration-Induced)",
                Description = "Cracking from cyclic stresses (vibration/water hammer) below yield strength. Initiates at stress risers (welds, notches).",
                AffectedMaterials = "All alloys (no endurance limit in SS/Al).",
                AffectedUnits = "Small-bore piping, socket welds, pump/compressor shafts, control valves, heat exchanger tubes.",
                CriticalFactors = "Stress concentration (welds/notches), vibration frequency/resonance, inclusions, microstructure.",
                Prevention = "Design improvements (gussets/supports), smooth weld transitions, radius edges on shafts, vibration dampeners.",
                Inspection = "PT/MT/WFMT for cracks; UT (angle/compression wave); vibration monitoring; audible/visual checks.",
                RiskLevel = "Medium-High (depends on cyclic stress)",
                RelatedMechanisms = new List<string> {        "Thermal Fatigue",        "Corrosion Fatigue"
    }
            });



            AddMechanism(new DamageMechanism
            {
                Name = "Metal Dusting",
                Description = "Rapid pitting/carburization (up to 1000 mpy) in CO/H2-rich streams at 900–1500°F, forming carbon/metal dust.",
                AffectedMaterials = "Low-alloy steels, 300 series SS, nickel alloys (none fully immune).",
                AffectedUnits = "Syn gas/POX units, reformer heater outlets, methanol reactors, gas turbines.",
                CriticalFactors = "Carbon activity >1, 900–1500°F (480–815°C), reducing conditions, alloy grain boundaries.",
                Prevention = "Sulfur injection (<10 ppm H2S), refractory lining, aluminized coatings, steam-to-carbon ratio >2–3.",
                Inspection = "VT/RT for pitting; filter effluent for metal dust; destructive testing.",
                RiskLevel = "High (extreme penetration rates)",
                RelatedMechanisms = new List<string> {        "Carburization",        "Oxidation"
    }
            });



            AddMechanism(new DamageMechanism
            {
                Name = "Microbiologically Influenced Corrosion (MIC)",
                Description = "Corrosion caused by living organisms such as bacteria, algae, or fungi, often associated with tubercles or slimy organic substances. Typically manifests as localized pitting or crevice corrosion.",
                AffectedMaterials = "Carbon steel, alloy steels, 300/400 series SS, aluminum alloys, copper alloys, nickel alloys. Titanium is highly resistant.",
                AffectedUnits = "Water-cooled heat exchangers, storage tanks, stagnant/low-flow piping, hydrotested equipment, fire-water systems.",
                CriticalFactors = "Stagnant/low-flow conditions, wide pH/temperature/salinity tolerance, nutrient availability (e.g., sulfur, hydrocarbons), biofilm formation.",
                Prevention = "Biocide treatment (chlorine, ozone), periodic flushing/cleaning, maintain flow >3 fps, slope pipes for drainage, coatings/cathodic protection for tanks.",
                Inspection = "Microbe counts, ATP/qPCR analysis, corrosion coupons, RFT for heat exchangers, VT/AUT/RT for localized damage.",
                RiskLevel = "High (rapid localized damage)",
                RelatedMechanisms = new List<string> {        "Cooling water corrosion",        "Brine corrosion",        "Oxygenated Process Water Corrosion"
    }
            });



            AddMechanism(new DamageMechanism
            {
                Name = "Naphthenic Acid Corrosion (NAC)",
                Description = "High-temperature corrosion caused by organic acids in crude oil, resulting in localized pitting or flow-induced grooving in turbulent areas.",
                AffectedMaterials = "Carbon steel, low-alloy steels, 400 series SS (vulnerable); 316L/317L SS, 6% Mo alloys, Alloy 625 (resistant).",
                AffectedUnits = "Crude/vacuum heater tubes, transfer lines, HVGO circuits, vacuum tower internals, pump internals/valves/elbows.",
                CriticalFactors = "TAN >0.3 (but can occur at 0.1), temperatures 350–800°F, high velocity/turbulence, low sulfur content.",
                Prevention = "Crude blending (reduce TAN), upgrade to Mo-rich alloys (e.g., 317L), phosphorus-based inhibitors (catalyst considerations).",
                Inspection = "VT/RT/UT scanning for localized thinning, Fe/Ni stream monitoring, ER probes (caution: turbulence effects).",
                RiskLevel = "High (severe in high-velocity zones)",
                RelatedMechanisms = new List<string> {        "Sulfidation",        "Erosion/Erosion-Corrosion"
    }
            });



            AddMechanism(new DamageMechanism
            {
                Name = "Nitriding",
                Description = "Formation of hard, brittle surface layer due to nitrogen diffusion at high temperatures, leading to microcracking and embrittlement.",
                AffectedMaterials = "Carbon steel, low-alloy steels, 400/300 series SS. Nickel alloys (>30% Ni) are resistant.",
                AffectedUnits = "Steam-methane reformers, ammonia synthesis plants, furnace tubes.",
                CriticalFactors = "Temperatures >600°F (severe >900°F), high nitrogen partial pressure, prolonged exposure.",
                Prevention = "Upgrade to nickel-based alloys (30–80% Ni). Process modification is impractical.",
                Inspection = "Surface hardness testing, metallography (confirm nitride layer), ECT for early detection, PT/MT for cracks.",
                RiskLevel = "Medium (uncommon in refining)",
                RelatedMechanisms = new List<string> {        "Carburization",        "Metal dusting"
    }
            });



            AddMechanism(new DamageMechanism
            {
                Name = "Oxidation",
                Description = "High-temperature scaling of metal surfaces due to reaction with oxygen, leading to general wall thinning.",
                AffectedMaterials = "All iron-based materials. Resistance increases with Cr content (e.g., 300 series SS resists up to 1500°F).",
                AffectedUnits = "Fired heaters, boilers, combustion equipment, high-temperature piping.",
                CriticalFactors = "Temperature (>1000°F for CS), alloy Cr content, water vapor accelerates oxidation in 9Cr-1Mo.",
                Prevention = "Upgrade to high-Cr alloys (e.g., 310 SS), use Si/Al-rich alloys for supports/burner tips.",
                Inspection = "Tube-skin thermocouples, IR thermography, UT/RT for wall loss, EMAT for external scaling.",
                RiskLevel = "Medium (predictable rates)",
                RelatedMechanisms = new List<string> {        "Sulfidation",        "Carburization"
    }
            });



            AddMechanism(new DamageMechanism
            {
                Name = "Oxygenated Process Water Corrosion",
                Description = "Accelerated corrosion due to dissolved oxygen in water, causing pitting or general thinning in low-temperature systems.",
                AffectedMaterials = "Carbon steel, low-alloy steels.",
                AffectedUnits = "Vacuum unit ejector coolers, flare gas recovery systems, compressor inlet piping, water/vapor interfaces.",
                CriticalFactors = "O₂ >20 ppb, low temperatures (higher O₂ solubility), velocity >10 fps (turbulence), stagnant zones.",
                Prevention = "Eliminate air in-leakage, oxygen scavengers (if leaks persist), filming amines, internal coatings.",
                Inspection = "UT scanning/AUT for localized pits, focus on turbulent areas (elbows, valves), O₂ monitoring (specialized sampling).",
                RiskLevel = "High (rapid pitting in aerated systems)",
                RelatedMechanisms = new List<string> {        "Cooling water corrosion",        "Concentration cell corrosion"
    }
            });




            AddMechanism(new DamageMechanism
            {
                Name = "Phenol (Carbolic Acid) Corrosion",
                Description = "Corrosion of carbon steel and other materials in phenol solvent systems used to remove aromatic compounds from lubricating oil feedstocks. Typically occurs in extraction and recovery sections.",
                AffectedMaterials = "Carbon steel, 304L, 316L, Alloy C276 (in order of increasing resistance).",
                AffectedUnits = "Phenol extraction facilities, lube oil plants, recovery towers, extract dryer condensers, overhead piping circuits.",
                CriticalFactors = "Temperature (>350°F/175°C accelerates corrosion), water content (5–15% phenol solutions are highly corrosive), flow velocity (>30 ft/s causes erosion-corrosion).",
                Prevention = "Use 316L stainless steel for critical areas (e.g., dryer towers, condenser shells); limit carbon steel velocity to <30 ft/s; maintain overhead temperatures 30°F above dew point.",
                Inspection = "VT for internal surfaces, UT thickness mapping, RT for localized attack, ER probes/coupons for monitoring.",
                RiskLevel = "Moderate to High (rapid thinning at high temps)",
                RelatedMechanisms = new List<string> {        "Erosion/Erosion-Corrosion",        "Aqueous organic acid corrosion",

    }
            });



            AddMechanism(new DamageMechanism
            {
                Name = "Phosphoric Acid Corrosion",
                Description = "Localized corrosion of carbon steel and stainless steels due to water-contaminated phosphoric acid catalysts in polymerization units. Severe thinning can occur rapidly.",
                AffectedMaterials = "Carbon steel, 304L SS, 316L SS, Alloy 20, Alloy 825 (increasing resistance).",
                AffectedUnits = "Polymerization unit piping, low-velocity areas (dead-legs, reboiler bottoms), manifolds, exchangers.",
                CriticalFactors = "Free water presence (even traces), acid concentration, temperature, halide contaminants (e.g., chlorides), shutdown washing operations.",
                Prevention = "Limit water to <400 ppm; upgrade to 316L SS (up to 225°F/105°C) or Alloy 825 (higher temps); eliminate stagnant zones.",
                Inspection = "UT/RT for thinning; monitor iron content, pH, and moisture in overhead receivers; ER probes/coupons in water draws.",
                RiskLevel = "High (¼-inch penetration in 8 hours with water)",
                RelatedMechanisms = new List<string> {        "Under-Deposit Corrosion",        "Intergranular Corrosion",        "Chloride Stress Corrosion Cracking"
    }
            });



            AddMechanism(new DamageMechanism
            {
                Name = "Polythionic Acid Stress Corrosion Cracking (PTA SCC)",
                Description = "Intergranular SCC in sensitized austenitic SS/Ni alloys when sulfide scales react with air/moisture during shutdowns, forming corrosive sulfur acids.",
                AffectedMaterials = "Sensitized 300 series SS, Alloy 600/800; stabilized grades (321/347 SS, Alloy 625/825) resist.",
                AffectedUnits = "FCC units (cyclones, air rings), hydroprocessing (reactors, heater tubes), crude/coker heater tubes, boiler components.",
                CriticalFactors = "Sensitization (700–1500°F), sulfide scale, tensile stress (welds), air/moisture exposure during shutdowns.",
                Prevention = "Alkaline wash/N₂ purge during shutdowns; use low-carbon 'L' grades or stabilized alloys (321/347 SS); thermal stabilization (1650°F PWHT).",
                Inspection = "PT (post-flapper wheel grinding), ECT for surface cracks, AET/angle beam UT for embedded cracks, FMR for sensitization.",
                RiskLevel = "Critical (rapid wall penetration)",
                RelatedMechanisms = new List<string> {        "Intergranular Corrosion",        "Chloride Stress Corrosion Cracking (Cl-SCC)"
    }
            });



            AddMechanism(new DamageMechanism
            {
                Name = "Refractory Degradation",
                Description = "Mechanical damage (cracking/spalling) or chemical corrosion (oxidation/sulfidation) of refractory linings, leading to high base metal temperatures.",
                AffectedMaterials = "Ceramic fibers, castables, refractory bricks, plastic refractories.",
                AffectedUnits = "FCC reactor-regenerators, coker units, heater fireboxes, sulfur plant waste heat boilers.",
                CriticalFactors = "Poor installation/dryout, thermal cycling, erosion (catalyst/ash), coke deposits, anchor material incompatibility.",
                Prevention = "Proper anchor design (oxidation-resistant); follow ASTM dryout schedules; monitor process upsets/cyclic operation.",
                Inspection = "VT for cracks/spalling; IR thermography for hot spots; hammer testing for channeling (FCC units).",
                RiskLevel = "Moderate (indirect risk via base metal overheating)",
                RelatedMechanisms = new List<string> {        "Oxidation",        "Sulfidation",        "Carburization"
    }
            });



            AddMechanism(new DamageMechanism
            {
                Name = "Stress Relaxation Cracking (Reheat Cracking)",
                Description = "Intergranular cracking in Cr-Mo steels, austenitic SS, and Ni alloys due to insufficient creep ductility during stress relief (PWHT) or high-temp service.",
                AffectedMaterials = "2¼Cr-1Mo-V, 1Cr-½Mo, 304H/316H SS, Alloy 800H/617.",
                AffectedUnits = "Hydroprocessing reactors, reformer heater tubes, high-pressure steam piping (>900°F/480°C).",
                CriticalFactors = "High residual/applied stress, coarse grain HAZ, impurity elements (P/Sn/Sb), thick sections, rapid heating rates.",
                Prevention = "Minimize stress concentrations; use low heat input welding; thermal stabilization (Alloy 800H); follow API 934-E for PWHT.",
                Inspection = "WFMT/PT for surface cracks; TOFD/PAUT for embedded cracks; monitor pipe loads/peaking.",
                RiskLevel = "High (catastrophic in heavy-wall components)",
                RelatedMechanisms = new List<string> {        "Creep and Stress Rupture",        "Intergranular Corrosion",        "Hydrogen Embrittlement"
    }
            });




            AddMechanism(new DamageMechanism
            {
                Name = "Short-term Overheating—Stress Rupture (Including Steam Blanketing)",
                Description = "Permanent deformation and failure due to localized overheating, resulting in bulging or 'fishmouth' ruptures. Occurs rapidly at very high temperatures or via creep at lower temperatures.",
                AffectedMaterials = "All fired heater/boiler tube materials (e.g., carbon steel, Cr-Mo alloys, stainless steels).",
                AffectedUnits = "Fired heaters (crude/vacuum/coker units), boilers, waste heat exchangers, hydroprocessing/reactor beds, catalytic reformers.",
                CriticalFactors = "Flame impingement, steam blanketing (DNB), process-side starvation, high internal pressure, pre-existing thinning from corrosion.",
                Prevention = "Minimize temperature excursions; use diffuse-flame burners; maintain BFW quality; install bed thermocouples in reactors; monitor refractory condition.",
                Inspection = "IR thermography for hot spots; VT/UT for bulging; FMR for creep voids; boroscope for steam blanketing damage.",
                RiskLevel = "High (catastrophic failure within hours)",
                RelatedMechanisms = new List<string> {        "Creep and Stress Rupture",        "Caustic Corrosion (Boilers)",        "Oxidation"
    }
            });



            AddMechanism(new DamageMechanism
            {
                Name = "Sigma Phase Embrittlement",
                Description = "Loss of ductility in stainless steels/Ni alloys due to sigma phase precipitation at 1000–1700°F (540–925°C). Causes brittle cracking during shutdowns.",
                AffectedMaterials = "300/400 series SS, duplex SS, HK/HP cast alloys, Alloy 800H/617. Weld metals with high ferrite content are most susceptible.",
                AffectedUnits = "FCC regenerator cyclones, heater tubes, weld overlays on Cr-Mo reactors, high-temperature piping.",
                CriticalFactors = "Time at temperature (>1000°F), high Cr/Mo content, ferrite phase in welds, cooling below 500°F (260°C).",
                Prevention = "Limit weld ferrite (5–9%); avoid PWHT exposure; use solution annealing (1950°F) where feasible; minimize stress during cooldown.",
                Inspection = "Metallography (KOH etching); Charpy impact testing; ECT for duplex SS; FMR for surface sigma.",
                RiskLevel = "Moderate (brittle fracture risk during shutdowns)",
                RelatedMechanisms = new List<string> {        "885°F (475°C) Embrittlement",        "Intergranular Corrosion",        "Stress Relaxation Cracking"
    }
            });



            AddMechanism(new DamageMechanism
            {
                Name = "Soil Corrosion",
                Description = "External thinning/pitting of buried carbon steel due to electrochemical reactions with soil. Most severe at soil-air interfaces.",
                AffectedMaterials = "Carbon steel, cast iron, ductile iron. Coated/CP systems resist damage.",
                AffectedUnits = "Underground piping, tank bottoms, ground-supported structures, road crossings.",
                CriticalFactors = "Low resistivity, high moisture/salt content, acidic soils, coating defects, stray currents, MIC.",
                Prevention = "Combined coatings + cathodic protection; proper backfill; avoid soil accumulation on aboveground piping.",
                Inspection = "Close-interval CP surveys; coating holiday detection; GWT/UT for metal loss; excavation for VT.",
                RiskLevel = "Moderate (localized pitting can be rapid)",
                RelatedMechanisms = new List<string> {
        "Galvanic Corrosion",
        "Microbiologically Influenced Corrosion (MIC)",
        "Concentration Cell Corrosion"
    }
            });



            AddMechanism(new DamageMechanism
            {
                Name = "Sour Water Corrosion (Acidic)",
                Description = "Carbon steel corrosion in H₂S-containing water (pH 4.5–7.0). Pitting occurs under porous sulfide films or with oxygen ingress.",
                AffectedMaterials = "Carbon steel. 300 series SS resists general attack but may pit.",
                AffectedUnits = "FCC/coker overhead systems, gas fractionation units.",
                CriticalFactors = "H₂S concentration, pH (<4.5 accelerates corrosion), oxygen contamination, CO₂/organic acids.",
                Prevention = "Monitor H₂S/pH/chlorides; inject polysulfide for cyanide control; use 300 series SS below 140°F (60°C).",
                Inspection = "UT/RT for thinning; corrosion coupons/probes; process parameter trending.",
                RiskLevel = "Moderate (localized under-deposit attack)",
                RelatedMechanisms = new List<string> {
        "Wet H₂S Damage",
        "CO₂ Corrosion",
        "Ammonium Bisulfide Corrosion"
    }
            });



            AddMechanism(new DamageMechanism
            {
                Name = "Spheroidization (Softening)",
                Description = "Microstructural change in steels (850–1400°F/440–760°C) where carbides agglomerate, reducing strength/creep resistance.",
                AffectedMaterials = "Carbon steel, C-½Mo, Cr-Mo alloys (1Cr–9Cr). Annealed/normalized steels degrade differently.",
                AffectedUnits = "Hot-wall FCC/reformer/coker piping, boiler/heater tubes.",
                CriticalFactors = "Temperature/time exposure, initial microstructure (fine-grained steels degrade faster).",
                Prevention = "Minimize long-term high-temp exposure; prefer annealed/coarse-grained steels.",
                Inspection = "FMR/metallography; hardness testing (indirect indicator).",
                RiskLevel = "Low (rarely causes direct failure)",
                RelatedMechanisms = new List<string> {
        "Graphitization",
        "Creep and Stress Rupture",
        "Temper Embrittlement"
    }
            });



            AddMechanism(new DamageMechanism
            {
                Name = "Strain Aging",
                Description = "Metallurgical damage in older carbon steels and C-½Mo low-alloy steels due to deformation and aging at intermediate temperatures, leading to increased hardness/strength but reduced ductility/toughness. Increases brittle fracture risk.",
                AffectedMaterials = "Older (pre-1980) carbon steels with large grain size and C-0.5Mo low-alloy steel.",
                AffectedUnits = "Thick-wall vessels made from susceptible materials without stress relief.",
                CriticalFactors = "Steel composition/manufacturing process (Bessemer/open hearth), nitrogen levels, cold work without stress relief, welding repairs without PWHT, pressurization sequence vs. temperature.",
                Prevention = "Use modern low-nitrogen/aluminum-deoxidized steels; apply PWHT to weld repairs; follow ASME BPVC UCS-66 pressurization guidelines for susceptible equipment.",
                Inspection = "No online detection; hardness testing for large-area aging (not effective for weld-induced aging).",
                RiskLevel = "High (brittle fracture risk)",
                RelatedMechanisms = new List<string> {

        "Temper Embrittlement", "Brittle Fracture"
    }
            });



            AddMechanism(new DamageMechanism
            {
                Name = "Sulfidation",
                Description = "High-temperature corrosion from sulfur compounds (without hydrogen), causing uniform thinning or rupture. Forms sulfide scales; low-Si carbon steel is highly susceptible.",
                AffectedMaterials = "Iron-based alloys (carbon steel > 400 series SS > 300 series SS), nickel alloys (worse above 1193°F/645°C), copper alloys.",
                AffectedUnits = "Crude/vacuum/FCC/coker units, feed sections of hydroprocessing units, heaters fired with high-sulfur fuels.",
                CriticalFactors = "Temperature (>500°F/260°C), reactive sulfur content, silicon content (<0.10% Si accelerates corrosion), chromium content (improves resistance).",
                Prevention = "Upgrade to 9Cr-1Mo or 300 series SS; verify Si content; use PMI for alloy checks; avoid low-Si piping.",
                Inspection = "UT/RT thickness measurements; smart pigging for tubes; verify operating temps/sulfur levels; MVP programs.",
                RiskLevel = "High (rupture risk)",
                RelatedMechanisms = new List<string> {
        "High-temperature H₂/H₂S corrosion"
    }
            });



            AddMechanism(new DamageMechanism
            {
                Name = "Sulfuric Acid Corrosion",
                Description = "General/localized corrosion by sulfuric acid, exacerbated by velocity, contamination, or oxidizers. Includes hydrogen grooving in stagnant areas.",
                AffectedMaterials = "Carbon steel (worst), 316L SS, Alloy 20, high-Si cast iron, Alloy B-2/C-276 (best).",
                AffectedUnits = "Sulfuric acid alkylation units (contactors, reactor effluent lines), wastewater plants, storage tanks.",
                CriticalFactors = "Acid concentration (dilute <65% worst), temperature, velocity (>2-3 fps removes protective scale), oxidizers (e.g., O₂), mixing points.",
                Prevention = "Material selection (e.g., Alloy 20/C-276); control velocity; neutralize acid carryover; avoid mixing concentrated acid with water.",
                Inspection = "UT/RT for localized corrosion; coupons/ER probes; post-cleaning inspection for tanks.",
                RiskLevel = "High (rapid metal loss)",
                RelatedMechanisms = new List<string> { "Hydrochloric Acid (HCl) Corrosion", "Hydrogen Grooving", "Under-deposit Corrosion" }
            });



            AddMechanism(new DamageMechanism
            {
                Name = "Temper Embrittlement",
                Description = "Loss of fracture toughness in low-alloy steels after long-term exposure to 650–1070°F (345–575°C). Causes brittle fracture risk during start-up/shutdown.",
                AffectedMaterials = "2.25Cr-1Mo (especially pre-1972), 3Cr-1Mo, HSLA Cr-Mo-V rotor steels; weld metal is more susceptible.",
                AffectedUnits = "Hydroprocessing reactors, hot feed/effluent exchangers, catalytic reformers, FCC reactors.",
                CriticalFactors = "Impurity elements (P, Sn, Sb, As); Mn/Si content; exposure time/temperature; J/X factor thresholds.",
                Prevention = "Limit impurities (J≤100, X≤15); enforce MPT during start-up; de-embrittle via 1150°F/620°C heat treatment.",
                Inspection = "Monitor CVN impact test blocks; enforce pressurization/temperature sequences.",
                RiskLevel = "Medium (few industry failures but catastrophic potential)",
                RelatedMechanisms = new List<string> {
        "Brittle fracture"
    }
            });



            AddMechanism(new DamageMechanism
            {
                Name = "Thermal Fatigue",
                Description = "Cracking from cyclic stresses due to temperature variations. Initiated at stress concentrators (welds, nozzles).",
                AffectedMaterials = "All materials (common in carbon steel, SS).",
                AffectedUnits = "Mix points (H₂/hydrocarbon), coke drums, steam systems (desuperheaters), soot blowers.",
                CriticalFactors = "Temperature swing (>200–300°F/110–165°C), cycling frequency, constraint (rigid attachments), condensate impingement.",
                Prevention = "Design flexibility (slip spacers); blend grind welds; control heating/cooling rates; drain soot blowers.",
                Inspection = "PT/MT/WFMT for surface cracks; angle beam UT/TOFD for internal cracks; AET for active cracking.",
                RiskLevel = "High (equipment-specific)",
                RelatedMechanisms = new List<string> {
    "Mechanical fatigue",
    "Corrosion fatigue",
    "Dissimilar Metal Weld (DMW) Cracking",
    "Thermal Shock"
}
            });



            AddMechanism(new DamageMechanism
            {
                Name = "Thermal Shock",
                Description = "Cracking due to high, non-uniform thermal stresses from rapid temperature changes, often when cold liquid contacts hot metal surfaces (e.g., fire-water quenching).",
                AffectedMaterials = "All metals and alloys (stainless steels more susceptible due to higher thermal expansion coefficients).",
                AffectedUnits = "FCC, coker, catalytic reforming, hydroprocessing units; high-temperature piping; heavy-wall machinery (e.g., 12Cr castings); Cr-Mo equipment (if embrittled).",
                CriticalFactors = "Large temperature differentials, material thickness (thick sections prone to gradients), restraint preventing expansion/contraction, casting flaws (initiation sites).",
                Prevention = "Minimize water contact with hot equipment (rain/fire deluge), design to reduce restraint, install thermal sleeves/protective measures.",
                Inspection = "VT (surface cracks), PT, MT, UT (localized cracking is unpredictable; methods used post-event like fires/storms).",
                RiskLevel = "High (catastrophic potential)",
                RelatedMechanisms = new List<string> {
    "Thermal fatigue",
    "Short-term Overheating—Stress Rupture (Including Steam Blanketing)",
    "Brittle fracture"
}
            });



            AddMechanism(new DamageMechanism
            {
                Name = "Titanium Hydriding",
                Description = "Hydrogen absorption forming brittle hydride phases in titanium, leading to loss of ductility and brittle fracture (no visible corrosion).",
                AffectedMaterials = "Titanium and titanium alloys (alpha-beta alloys like Gr. 2/7/12/16; beta alloys more resistant but rarely used).",
                AffectedUnits = "Heat exchangers/air coolers (SWS, amine overhead condensers); hydrogen atmospheres >350°F (175°C); cathodically protected equipment.",
                CriticalFactors = "Temperature (>165°F/75°C), pH extremes (<3 or >8), H₂S presence, galvanic contact with carbon steel/300SS, embedded steel corrosion products.",
                Prevention = "Avoid galvanic couples (use all-titanium/isolated designs), monitor known hydriding services (amine/SWS), caution during maintenance (brittle fracture risk).",
                Inspection = "VT (cracks), destructive testing (bend/crush/impact tests), metallurgical analysis; ECT (experimental, validate first).",
                RiskLevel = "High (sudden failure during maintenance)",
                RelatedMechanisms = new List<string> {
        "Hydrogen embrittlement",
        "Brittle fracture"
    }
            });



            AddMechanism(new DamageMechanism
            {
                Name = "Wet H₂S Damage (Blistering/HIC/SOHIC/SSC)",
                Description = "Four mechanisms: blistering (H₂ trapped at inclusions), HIC (internal stepwise cracks), SOHIC (stacked HIC + stress), SSC (cracking under tensile stress in hard steels).",
                AffectedMaterials = "Carbon steel (blistering/HIC/SOHIC); low-alloy/martensitic stainless steels (SSC).",
                AffectedUnits = "Refinery-wide (wet H₂S environments); hydroprocessing (NH₄HS >2%), FCC/coker overheads (cyanides), SWS/amine regenerators; high-strength components (bolts/valves).",
                CriticalFactors = "H₂S (>50 ppmw, but <1 ppm possible), pH (minimal at pH7, worse at extremes), cyanides/contaminants, temperature (SSC <200°F/95°C), hardness (>22 HRC for SSC), inclusions (HIC).",
                Prevention = "HIC-resistant steels, PWHT (SOHIC/SSC), hardness control (<200 HB for welds), coatings/cladding, process changes (wash water/polysulfide injection).",
                Inspection = "VT (blisters/cracks), WFMT/ECT/ACFM (SOHIC), UT (angle beam for cracks), monitor water chemistry (pH/H₂S/HCN).",
                RiskLevel = "High (SOHIC/SSC can cause rapid failure)",
                RelatedMechanisms = new List<string> {    "Hydrogen embrittlement",    "Amine Stress Corrosion Cracking (Amine SCC)",    "Carbonate Stress Corrosion Cracking (ACSCC)",    "Stress corrosion cracking (SCC)"
}
            });

            // 1. Wet H₂S Blistering
            AddMechanism(new DamageMechanism
            {
                Name = "Wet H₂S - Blistering",
                Description = "Hydrogen atoms from H₂S corrosion diffuse into steel and accumulate at inclusions/laminations, forming molecular H₂ that creates internal pressure blisters. Typically occurs in low-strength carbon steel.",
                AffectedMaterials = "Carbon steels (especially non-HIC resistant grades with high inclusion content), low-alloy steels.",
                AffectedUnits = "Refinery sour water systems, amine units, FCC/coker overheads, hydroprocessing units (NH₄HS >2%), any wet H₂S service.",
                CriticalFactors = "H₂S concentration (>50 ppmw accelerates), pH (<6 increases risk), temperature (30-150°F most severe), steel cleanliness (MnS inclusions), stress (blisters form at stress concentrators).",
                Prevention = "Use HIC-resistant steels (NACE TM0284 compliant), control hardness (<200 HB), post-weld heat treatment, polysulfide injection to convert H₂S, proper drainage to avoid wet conditions.",
                Inspection = "Visual inspection (bulges), UT thickness mapping, wet fluorescent MT for surface cracks near blisters, acoustic emission testing for active blister growth.",
                RiskLevel = "Medium (can lead to leaks but rarely catastrophic)",
                RelatedMechanisms = new List<string> {
    "Hydrogen Induced Cracking (HIC)",
    "Stress-Oriented Hydrogen Induced Cracking (SOHIC)",
    "Wet H₂S Damage (Blistering/HIC/SOHIC/SSC)",
    "Hydrogen blistering"
}
            });

            // 2. Hydrogen Induced Cracking (HIC)
            AddMechanism(new DamageMechanism
            {
                Name = "Wet H₂S - Hydrogen Induced Cracking (HIC)",
                Description = "Stepwise internal cracks parallel to the surface caused by hydrogen recombination at elongated inclusions (typically MnS). Unlike blistering, HIC connects multiple inclusion sites without surface bulging.",
                AffectedMaterials = "Carbon steels (especially non-HIC resistant grades), steels with high sulfur content (>0.003% ideal).",
                AffectedUnits = "Sour water strippers, amine contactors, crude distillation overheads, hydrotreater effluent streams.",
                CriticalFactors = "H₂S (>50 ppm), pH (<6), cyanides (accelerate H absorption), temperature (peaks at 80°F), steel cleanliness (centerline segregation in plate).",
                Prevention = "Ultra-low sulfur steels (NACE TM0284 Grade A/B/C), calcium-treated steels to spheroidize inclusions, PWHT, control hardness (<200 HB).",
                Inspection = "Ultrasonic testing (straight beam for internal cracks), advanced UT (TOFD, phased array), metallography on coupons, real-time H₂ monitoring.",
                RiskLevel = "Medium-High (can cause sudden wall loss)",
                RelatedMechanisms = new List<string> {

    "Stress-Oriented Hydrogen Induced Cracking (SOHIC)",
    "Wet H₂S Damage (Blistering/HIC/SOHIC/SSC)"
}
            });

            // 3. Stress-Oriented Hydrogen Induced Cracking (SOHIC)
            AddMechanism(new DamageMechanism
            {
                Name = "Wet H₂S - Stress-Oriented Hydrogen Induced Cracking (SOHIC)",
                Description = "Stacked arrays of HIC cracks linked by through-thickness cracks in the through-thickness direction, driven by residual/applied stresses. Most dangerous Wet H₂S mechanism.",
                AffectedMaterials = "Carbon steels (welds/HAZ most susceptible), low-alloy steels.",
                AffectedUnits = "Pressure vessel welds (especially circumferential), heat affected zones, high-stress regions in wet H₂S service.",
                CriticalFactors = "Residual stresses (welds without PWHT), H₂S concentration (>50 ppm), hardness (>200 HB), thickness (>20mm increases constraint).",
                Prevention = "Post-weld heat treatment (PWHT), use SOHIC-resistant steels (NACE MR0175/ISO 15156), control weld hardness (<200 HB), avoid notch geometries.",
                Inspection = "Advanced UT (TOFD, phased array), angled UT scans, ACFM for surface-breaking cracks, monitor for leaks.",
                RiskLevel = "Very High (can cause catastrophic brittle fracture)",
                RelatedMechanisms = new List<string> {
    "Hydrogen Induced Cracking (HIC)",
    "Wet H₂S Damage (Blistering/HIC/SOHIC/SSC)",
    "Brittle fracture"
}
            });

            // 4. Sulfide Stress Cracking (SSC)
            AddMechanism(new DamageMechanism
            {
                Name = "Wet H₂S - Sulfide Stress Cracking (SSC)",
                Description = "Brittle failure of high-strength steels under tensile stress in H₂S environments. Differs from other Wet H₂S mechanisms by requiring both high hardness and stress.",
                AffectedMaterials = "High-strength steels (>22 HRC), martensitic stainless steels (400 series), cold-worked components, hard welds.",
                AffectedUnits = "Bolting, valve stems, springs, high-pressure vessels, wellhead equipment in refining/petrochemical.",
                CriticalFactors = "Hardness (>22 HRC critical threshold), H₂S (as low as 1 ppm), pH (<7.6), temperature (<140°F/60°C most severe), stress level (>30% yield).",
                Prevention = "Hardness control (<22 HRC per NACE MR0175), PWHT, use CRAs (Corrosion Resistant Alloys) like Inconel, coatings/cathodic protection.",
                Inspection = "Hardness testing (all suspect components), WFMT/MPT for surface cracks, UT for subsurface defects, hydrogen probes.",
                RiskLevel = "Critical (sudden failure without warning)",
                RelatedMechanisms = new List<string> {
    "Hydrogen Embrittlement",
    "Stress corrosion cracking (SCC)",
    "Caustic SCC"
}

            });
            ValidateAllRelatedMechanisms();
        }

        private static void AddMechanism(DamageMechanism mechanism)
        {
            if (mechanism == null)
                throw new ArgumentNullException(nameof(mechanism));

            if (string.IsNullOrWhiteSpace(mechanism.Name))
                throw new ArgumentException("Mechanism name cannot be null or whitespace");

            if (!_mechanisms.ContainsKey(mechanism.Name))
            {
                _mechanisms.Add(mechanism.Name, mechanism);
            }
        }

        public static DamageMechanism GetMechanism(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return null;

            // Normalize the search name
            string normalizedSearch = NormalizeMechanismName(name);

            // First try exact match (case insensitive)
            var mechanism = _mechanisms.Values
                .FirstOrDefault(m => NormalizeMechanismName(m.Name)
                                    .Equals(normalizedSearch, StringComparison.OrdinalIgnoreCase));

            if (mechanism != null)
                return mechanism;

            // Fallback to "contains" match if exact fails
            return _mechanisms.Values
                .FirstOrDefault(m => NormalizeMechanismName(m.Name)
                                    .Contains(normalizedSearch));
        }

        private static string NormalizeMechanismName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return string.Empty;

            // Remove common variations and special characters
            return name.Replace("₂", "2")          // Subscript 2 to regular
                      .Replace("(HE)", "")        // Remove (HE) suffix
                      .Replace("(Cl-SCC)", "")    // Remove (Cl-SCC)
                      .Replace("(Amine SCC)", "") // Remove (Amine SCC)
                      .Replace("(Cr-Mo steels)", "") // Remove steel type
                      .Replace("(Gouging)", "")   // Remove (Gouging)
                      .Replace("(Including Steam Blanketing)", "") // Remove qualifier
                      .Replace("(Alkaline Sour Water)", "") // Remove water type
                      .Replace("(PTA SCC)", "")   // Remove (PTA SCC)
                      .Replace("(Reheat Cracking)", "") // Remove reheat
                      .Replace("(Ethanol SCC)", "") // Remove ethanol
                      .Replace("(HF SCC)", "")     // Remove HF
                      .Replace("(ACSCC)", "")      // Remove ACSCC
                      .Replace("(NAC)", "")        // Remove NAC
                      .Replace("(MIC)", "")        // Remove MIC
                      .Replace("(HTHA)", "")       // Remove HTHA
                      .Replace("(LME)", "")        // Remove LME
                      .Replace("(DMW)", "")       // Remove DMW
                      .Replace("(CUI)", "")        // Remove CUI
                      .Replace("—", " ")          // Replace em dash with space
                      .Replace("/", " ")          // Replace slashes with space
                      .Trim();
        }

        public static IReadOnlyList<DamageMechanism> GetAllMechanisms()
        {
            return _mechanisms.Values
                .OrderBy(m => m.Name, StringComparer.OrdinalIgnoreCase)
                .ToList()
                .AsReadOnly();
        }

        public static IReadOnlyList<string> GetAllMaterials()
        {
            return ExtractDistinctItems(_mechanisms.Values.Select(m => m.AffectedMaterials))
                .AsReadOnly();
        }

        public static IReadOnlyList<string> GetAllUnits()
        {
            return ExtractDistinctItems(_mechanisms.Values.Select(m => m.AffectedUnits))
                .AsReadOnly();
        }

        public static IReadOnlyList<string> GetMaterialCategories()
        {
            return _materialCategories
                .OrderBy(c => c)
                .ToList()
                .AsReadOnly();
        }

        private static List<string> ExtractDistinctItems(IEnumerable<string> sourceStrings)
        {
            if (sourceStrings == null)
                return new List<string>();

            return sourceStrings
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .SelectMany(s => s.Split(new[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries))
                .Select(item => item.Trim())
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .OrderBy(item => item, StringComparer.OrdinalIgnoreCase)
                .ToList();
        }



        public static IReadOnlyList<DamageMechanism> SearchMechanisms(string searchTerm = "")
        {
            var query = _mechanisms.Values.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(m =>
                    (m.Name?.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0) ||
                    (m.Description?.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0) ||
                    (m.AffectedMaterials?.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0) ||
                    (m.AffectedUnits?.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0) ||
                    (m.RelatedMechanisms?.Any(rm =>
                        rm?.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0) == true));
            }

            return query
                .OrderBy(m => m.Name, StringComparer.OrdinalIgnoreCase)
                .ToList()
                .AsReadOnly();
        }
        public static void ValidateAllRelatedMechanisms()
        {
            foreach (var mechanism in _mechanisms.Values)
            {
                if (mechanism.RelatedMechanisms != null)
                {
                    var invalidRefs = mechanism.RelatedMechanisms
                        .Where(relatedName => GetMechanism(relatedName) == null)
                        .ToList();

                    if (invalidRefs.Any())
                    {
                        Debug.WriteLine($"Invalid references in {mechanism.Name}: {string.Join(", ", invalidRefs)}");
                        // Optionally fix them automatically:
                        mechanism.RelatedMechanisms = mechanism.RelatedMechanisms
                            .Where(relatedName => GetMechanism(relatedName) != null)
                            .ToList();
                    }
                }
            }
        }
    }
}
