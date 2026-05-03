import { FormEvent, useEffect, useMemo, useState } from 'react';
import { ApiError, lwnLookupApi, pipeLookupApi, pressureVesselApi } from './api';
import type {
  AttachmentLocation,
  CodeEra,
  ConicalShellInput,
  ConicalShellResult,
  CylindricalShellInput,
  CylindricalShellResult,
  HeadThicknessInput,
  HeadThicknessResult,
  HeadType,
  NozzleThicknessInput,
  NozzleThicknessResult,
  NozzleType,
  SphericalShellInput,
  SphericalShellResult,
  Ug45TableEntry,
  PressureVesselMaterialStressInput
} from './types';

type Tab = 'shells' | 'heads' | 'nozzles';
type ShellGeom = 'cylindrical' | 'spherical' | 'conical';
type NozzleSizingMode = 'nps_schedule' | 'lwn' | 'custom';

const asNum = (v: string) => Number(v || 0);
const fmt = (v: number) => v.toFixed(4);

export function PressureVesselCalculatorPage() {
  const [tab, setTab] = useState<Tab>('shells');
  const [error, setError] = useState<string | null>(null);

  const [shellGeom, setShellGeom] = useState<ShellGeom>('cylindrical');
  const [headTypes, setHeadTypes] = useState<HeadType[]>([]);
  const [nozzleTypes, setNozzleTypes] = useState<NozzleType[]>([]);
  const [ug45Rows, setUg45Rows] = useState<Ug45TableEntry[]>([]);

  useEffect(() => { pressureVesselApi.getHeadTypes().then(setHeadTypes).catch(() => setHeadTypes([])); pressureVesselApi.getNozzleTypes().then(setNozzleTypes).catch(() => setNozzleTypes([])); pressureVesselApi.getUg45Table().then(setUg45Rows).catch(() => setUg45Rows([])); }, []);

  const [cyl, setCyl] = useState<CylindricalShellInput>({ designPressurePsi: 150, allowableStressPsi: 20000, insideDiameterIn: 48, outsideDiameterIn: 0, originalThicknessIn: 0.5, jointEfficiency: 1, corrosionAllowanceIn: 0.125, providedThicknessIn: 0.625 });
  const [sph, setSph] = useState<SphericalShellInput>({ designPressurePsi: 150, allowableStressPsi: 20000, insideDiameterIn: 48, outsideDiameterIn: 0, originalThicknessIn: 0.5, jointEfficiency: 1, corrosionAllowanceIn: 0.125, providedThicknessIn: 0.625 });
  const [cone, setCone] = useState<ConicalShellInput>({ designPressurePsi: 150, allowableStressPsi: 20000, effectiveInsideDiameterIn: 48, halfApexAngleDeg: 30, jointEfficiency: 1, corrosionAllowanceIn: 0.125, providedThicknessIn: 0.625 });
  const [shellResult, setShellResult] = useState<CylindricalShellResult | SphericalShellResult | ConicalShellResult | null>(null);
  const [shellStressInfo, setShellStressInfo] = useState<string>('');
  const [materialStress, setMaterialStress] = useState<PressureVesselMaterialStressInput>({ designCode:'ASME_VIII_DIV1', stressEra:'From1999Onward', designTemperatureF:300, materialSpec:'SA-TEST', materialGrade:'A', productForm:'Plate', alloyUNS:'', classConditionTemper:'', manualAllowableStress:true, allowableStressPsi:20000 });

  const [head, setHead] = useState<HeadThicknessInput>({ headType: 'Ellipsoidal2To1', designPressurePsi: 150, allowableStressPsi: 20000, jointEfficiency: 1, effectiveInsideDiameterIn: 48, effectiveInsideRadiusIn: 24, crownRadiusIn: 48, halfApexAngleDeg: 30, flatHeadCFactor: 0.3, corrosionAllowanceIn: 0.125, providedThicknessIn: 0.625 });
  const [headResult, setHeadResult] = useState<HeadThicknessResult | null>(null);

  const [noz, setNoz] = useState<NozzleThicknessInput>({ designCode: 'ASME Section VIII 1999', designPressurePsi: 150, externalPressurePsi: 0, designTemperatureF: 100, jointEfficiency: 1, corrosionAllowanceIn: 0.125, manualAllowableStress: false, allowableStressPsi: 20000, materialSpec: 'SA-106', materialGrade: 'B', materialProductForm: 'Seamless Pipe', codeEra: 'Post1999', attachmentLocation: 'Shell', shellOrHeadRequiredThicknessIn: 0.1, shellOrHeadExternalRequiredThicknessIn: 0, ug16MinimumThicknessIn: 0.0625, nozzleType: 'PipeNozzle', useOdForTa: true, useIdForTa: false, outsideDiameterIn: 4.5, insideDiameterIn: 4.0, nominalThicknessIn: 0.25, originalThicknessIn: 0.25, nominalPipeSize: '4', ug45TableMinimumThicknessIn: null });
  const [nozResult, setNozResult] = useState<NozzleThicknessResult | null>(null);

  const [nozzleSizingMode, setNozzleSizingMode] = useState<NozzleSizingMode>('nps_schedule');
  const [npsOptions, setNpsOptions] = useState<string[]>([]);
  const [npsSchedules, setNpsSchedules] = useState<string[]>([]);
  const [selectedNps, setSelectedNps] = useState<string>('4');
  const [selectedNpsSchedule, setSelectedNpsSchedule] = useState<string>('40');
  const [lwnSizes, setLwnSizes] = useState<string[]>([]);
  const [lwnSchedules, setLwnSchedules] = useState<string[]>([]);
  const [selectedLwnSize, setSelectedLwnSize] = useState<string>('4');
  const [selectedLwnSchedule, setSelectedLwnSchedule] = useState<string>('STD');



  useEffect(() => {
    pipeLookupApi.getNps().then(values => {
      setNpsOptions(values);
      if (values.length > 0 && !values.includes(selectedNps)) setSelectedNps(values[0]);
    }).catch(() => setNpsOptions([]));
    lwnLookupApi.getSizes().then(values => {
      setLwnSizes(values);
      if (values.length > 0 && !values.includes(selectedLwnSize)) setSelectedLwnSize(values[0]);
    }).catch(() => setLwnSizes([]));
  }, []);

  useEffect(() => {
    if (!selectedNps) return;
    pipeLookupApi.getSchedules(selectedNps).then(values => {
      setNpsSchedules(values);
      if (values.length > 0 && !values.includes(selectedNpsSchedule)) setSelectedNpsSchedule(values[0]);
    }).catch(() => setNpsSchedules([]));
  }, [selectedNps]);

  useEffect(() => {
    if (!selectedLwnSize) return;
    lwnLookupApi.getSchedules(selectedLwnSize).then(values => {
      setLwnSchedules(values);
      if (values.length > 0 && !values.includes(selectedLwnSchedule)) setSelectedLwnSchedule(values[0]);
    }).catch(() => setLwnSchedules([]));
  }, [selectedLwnSize]);

  const headFields = useMemo(() => ({
    dia: ['Ellipsoidal2To1', 'Conical', 'Toriconical', 'FlatUg34'].includes(head.headType),
    radius: head.headType === 'Hemispherical', crown: head.headType === 'TorisphericalAsmeFd', angle: ['Conical', 'Toriconical'].includes(head.headType), c: head.headType === 'FlatUg34'
  }), [head.headType]);

  const syncRelationship = (setter: any, data: any, name: string, value: number) => {
    const n = { ...data, [name]: value };
    const od = name === 'outsideDiameterIn' ? value : n.outsideDiameterIn;
    const id = name === 'insideDiameterIn' ? value : n.insideDiameterIn;
    const t = name === 'originalThicknessIn' || name === 'nominalThicknessIn' ? value : (n.originalThicknessIn ?? n.nominalThicknessIn);
    if (od > 0 && id > 0) { if ('originalThicknessIn' in n) n.originalThicknessIn = (od - id) / 2; if ('nominalThicknessIn' in n) n.nominalThicknessIn = (od - id) / 2; }
    else if (od > 0 && t > 0) n.insideDiameterIn = od - 2 * t;
    else if (id > 0 && t > 0) n.outsideDiameterIn = id + 2 * t;
    setter(n);
  };

  const handleErr = (e: unknown) => setError(e instanceof ApiError ? e.message : 'Unexpected error');

  return <div style={{ padding: 16 }}><h1>Pressure Vessel Calculator</h1>{error && <div style={{ color: 'crimson' }}>{error}</div>}
    <div style={{ display: 'flex', gap: 8, marginBottom: 12 }}>{(['shells', 'heads', 'nozzles'] as Tab[]).map(t => <button key={t} onClick={() => { setTab(t); setError(null); }}>{t === 'nozzles' ? 'Nozzles / UG-45' : t[0].toUpperCase() + t.slice(1)}</button>)}</div>
    {tab === 'shells' && <form onSubmit={async (e: FormEvent) => { e.preventDefault(); try { setError(null); if (shellGeom === 'conical') { const r = await pressureVesselApi.calculateConical({ input: cone, materialStress: null }); setShellResult(r.result); setShellStressInfo(`Manual-only for now unless material section is provided. Resolved allowable stress: ${r.resolvedAllowableStressPsi.toLocaleString()} psi.`); } else if (shellGeom === 'cylindrical') { const r = await pressureVesselApi.calculateCylindrical({ input: cyl, materialStress }); setShellResult(r.result); setShellStressInfo(`Resolved allowable stress: ${r.resolvedAllowableStressPsi.toLocaleString()} psi. ${r.stressSourceMessage}`); } else { const r = await pressureVesselApi.calculateSpherical({ input: sph, materialStress }); setShellResult(r.result); setShellStressInfo(`Resolved allowable stress: ${r.resolvedAllowableStressPsi.toLocaleString()} psi. ${r.stressSourceMessage}`); } } catch (e) { handleErr(e); } }}>
      <select value={shellGeom} onChange={e => setShellGeom(e.target.value as ShellGeom)}><option value='cylindrical'>Cylindrical</option><option value='spherical'>Spherical</option><option value='conical'>Conical</option></select>
      {shellGeom !== 'conical' && <><input value={(shellGeom==='cylindrical'?cyl:sph).insideDiameterIn} onChange={e=>syncRelationship(shellGeom==='cylindrical'?setCyl:setSph,shellGeom==='cylindrical'?cyl:sph,'insideDiameterIn',asNum(e.target.value))}/><input value={(shellGeom==='cylindrical'?cyl:sph).outsideDiameterIn} onChange={e=>syncRelationship(shellGeom==='cylindrical'?setCyl:setSph,shellGeom==='cylindrical'?cyl:sph,'outsideDiameterIn',asNum(e.target.value))}/><input value={(shellGeom==='cylindrical'?cyl:sph).originalThicknessIn} onChange={e=>syncRelationship(shellGeom==='cylindrical'?setCyl:setSph,shellGeom==='cylindrical'?cyl:sph,'originalThicknessIn',asNum(e.target.value))}/></>}
      {shellGeom === 'conical' && <input value={cone.effectiveInsideDiameterIn} onChange={e=>setCone({...cone,effectiveInsideDiameterIn:asNum(e.target.value)})}/>}
      {shellGeom === 'conical' && <input value={cone.halfApexAngleDeg} onChange={e=>setCone({...cone,halfApexAngleDeg:asNum(e.target.value)})}/>}
      <label><input type='checkbox' checked={materialStress.manualAllowableStress} onChange={e=>setMaterialStress({...materialStress,manualAllowableStress:e.target.checked})}/> Manual allowable stress override (psi)</label><input placeholder='Allowable stress psi' disabled={!materialStress.manualAllowableStress} value={(materialStress.allowableStressPsi ?? 0)} onChange={e=>setMaterialStress({...materialStress,allowableStressPsi:asNum(e.target.value)})}/>
      <input placeholder='Joint efficiency' value={(shellGeom==='cylindrical'?cyl:shellGeom==='spherical'?sph:cone).jointEfficiency} onChange={e=>shellGeom==='cylindrical'?setCyl({...cyl,jointEfficiency:asNum(e.target.value)}):shellGeom==='spherical'?setSph({...sph,jointEfficiency:asNum(e.target.value)}):setCone({...cone,jointEfficiency:asNum(e.target.value)})}/>
      <button type='submit'>Calculate</button>
      {shellStressInfo && <div>{shellStressInfo}</div>}{shellResult && <div>Formula: {fmt('formulaRequiredThicknessIn' in shellResult ? shellResult.formulaRequiredThicknessIn : shellResult.governingRequiredThicknessIn)} in; Required+CA: {fmt(shellResult.requiredWithCorrosionAllowanceIn)} in; Margin: {fmt(shellResult.marginIn)} in {('warnings' in shellResult ? shellResult.warnings : []).map((w:string)=><div key={w}>{w}</div>)}</div>}
    </form>}
    {tab === 'heads' && <form onSubmit={async e=>{e.preventDefault(); try {{ const r = await pressureVesselApi.calculateHead({ input: head, materialStress: null }); setHeadResult(r.result); } setError(null);} catch (e) {handleErr(e);} }}>
      <select value={head.headType} onChange={e=>setHead({...head,headType:e.target.value as HeadType})}>{(headTypes.length?headTypes:['Ellipsoidal2To1','Hemispherical','TorisphericalAsmeFd','Conical','Toriconical','FlatUg34'] as HeadType[]).map(h=><option key={h}>{h}</option>)}</select>
      {headFields.dia && <input value={head.effectiveInsideDiameterIn} onChange={e=>setHead({...head,effectiveInsideDiameterIn:asNum(e.target.value)})}/>} {headFields.radius && <input value={head.effectiveInsideRadiusIn} onChange={e=>setHead({...head,effectiveInsideRadiusIn:asNum(e.target.value)})}/>} {headFields.crown && <input value={head.crownRadiusIn} onChange={e=>setHead({...head,crownRadiusIn:asNum(e.target.value)})}/>} {headFields.angle && <input value={head.halfApexAngleDeg} onChange={e=>setHead({...head,halfApexAngleDeg:asNum(e.target.value)})}/>} {headFields.c && <input value={head.flatHeadCFactor} onChange={e=>setHead({...head,flatHeadCFactor:asNum(e.target.value)})}/>}
      <input placeholder='Allowable stress psi' value={head.allowableStressPsi} onChange={e=>setHead({...head,allowableStressPsi:asNum(e.target.value)})}/><input placeholder='Joint efficiency' value={head.jointEfficiency} onChange={e=>setHead({...head,jointEfficiency:asNum(e.target.value)})}/><button type='submit'>Calculate</button>
      {headResult && <div>Formula: {fmt(headResult.governingRequiredThicknessIn)} in; Required+CA: {fmt(headResult.requiredWithCorrosionAllowanceIn)} in; Margin: {fmt(headResult.marginIn)} in</div>}
    </form>}
    {tab === 'nozzles' && <form onSubmit={async e=>{e.preventDefault();try{{ const r = await pressureVesselApi.calculateNozzle({ input: noz }); setNozResult(r.result); }setError(null);}catch(e){handleErr(e)}}}>
      <select value={nozzleSizingMode} onChange={e=>setNozzleSizingMode(e.target.value as NozzleSizingMode)}>
        <option value='nps_schedule'>NPS / Schedule</option><option value='lwn'>Long Weld Neck</option><option value='custom'>Custom</option>
      </select>
      <select value={noz.nozzleType} onChange={e=>setNoz({...noz,nozzleType:e.target.value as NozzleType})}>{(nozzleTypes.length?nozzleTypes:['PipeNozzle','ForgedNozzle','LongWeldNeck','FittingNozzle'] as NozzleType[]).map(n=><option key={n}>{n}</option>)}</select>
      {nozzleSizingMode === 'nps_schedule' && <>
        <select value={selectedNps} onChange={e=>setSelectedNps(e.target.value)}>{npsOptions.map(v=><option key={v} value={v}>{v}</option>)}</select>
        <select value={selectedNpsSchedule} onChange={e=>setSelectedNpsSchedule(e.target.value)}>{npsSchedules.map(v=><option key={v} value={v}>{v}</option>)}</select>
        <button type='button' onClick={async ()=>{ try { const d = await pipeLookupApi.lookup({ nps: selectedNps, schedule: selectedNpsSchedule }); const row=ug45Rows.find(r=>r.nps===selectedNps); setNoz({ ...noz, outsideDiameterIn:d.outsideDiameter, insideDiameterIn:d.insideDiameter, nominalThicknessIn:d.nominalThickness, originalThicknessIn:d.nominalThickness, nominalPipeSize:selectedNps, ug45TableMinimumThicknessIn:row?.minimumThicknessIn??null }); setError(null);} catch(e){handleErr(e);} }}>Lookup NPS</button>
      </>}
      {nozzleSizingMode === 'lwn' && <>
        <select value={selectedLwnSize} onChange={e=>setSelectedLwnSize(e.target.value)}>{lwnSizes.map(v=><option key={v} value={v}>{v}</option>)}</select>
        <select value={selectedLwnSchedule} onChange={e=>setSelectedLwnSchedule(e.target.value)}>{lwnSchedules.map(v=><option key={v} value={v}>{v}</option>)}</select>
        <button type='button' onClick={async ()=>{ try { const d = await lwnLookupApi.lookup({ size: selectedLwnSize, schedule: selectedLwnSchedule }); const row=ug45Rows.find(r=>r.nps===selectedLwnSize); setNoz({ ...noz, outsideDiameterIn:d.outsideDiameter, insideDiameterIn:d.insideDiameter, nominalThicknessIn:d.nominalThickness, originalThicknessIn:d.nominalThickness, nominalPipeSize:selectedLwnSize, ug45TableMinimumThicknessIn:row?.minimumThicknessIn??null }); setError(null);} catch(e){handleErr(e);} }}>Lookup LWN</button>
      </>}
      <select value={noz.attachmentLocation} onChange={e=>setNoz({...noz,attachmentLocation:e.target.value as AttachmentLocation})}><option value='Shell'>Shell</option><option value='Head'>Head</option></select>
      <input value={noz.shellOrHeadRequiredThicknessIn} onChange={e=>setNoz({...noz,shellOrHeadRequiredThicknessIn:asNum(e.target.value)})} placeholder='Required shell/head thickness at nozzle location'/>
      <input value={noz.shellOrHeadExternalRequiredThicknessIn} onChange={e=>setNoz({...noz,shellOrHeadExternalRequiredThicknessIn:asNum(e.target.value)})} placeholder='External required thickness (optional)'/>
      <input value={noz.ug16MinimumThicknessIn} onChange={e=>setNoz({...noz,ug16MinimumThicknessIn:asNum(e.target.value)})} placeholder='UG-16 minimum thickness'/>
      <input value={noz.allowableStressPsi} onChange={e=>setNoz({...noz,allowableStressPsi:asNum(e.target.value)})} placeholder='Allowable stress psi'/><input value={noz.designPressurePsi} onChange={e=>setNoz({...noz,designPressurePsi:asNum(e.target.value)})} placeholder='Design pressure psi'/><input value={noz.jointEfficiency} onChange={e=>setNoz({...noz,jointEfficiency:asNum(e.target.value)})} placeholder='Joint efficiency'/><input value={noz.corrosionAllowanceIn} onChange={e=>setNoz({...noz,corrosionAllowanceIn:asNum(e.target.value)})} placeholder='Corrosion allowance in'/>
      <input value={noz.outsideDiameterIn} onChange={e=>syncRelationship(setNoz,noz,'outsideDiameterIn',asNum(e.target.value))}/><input value={noz.insideDiameterIn} onChange={e=>syncRelationship(setNoz,noz,'insideDiameterIn',asNum(e.target.value))}/><input value={noz.nominalThicknessIn} onChange={e=>syncRelationship(setNoz,noz,'nominalThicknessIn',asNum(e.target.value))}/>
      <input value={noz.originalThicknessIn} onChange={e=>setNoz({...noz,originalThicknessIn:asNum(e.target.value)})} placeholder='Provided original thickness in'/>
      <input value={noz.externalPressurePsi} onChange={e=>setNoz({...noz,externalPressurePsi:asNum(e.target.value)})} placeholder='External pressure psi'/>
      <label><input type='checkbox' checked={noz.useOdForTa} onChange={e=>setNoz({...noz,useOdForTa:e.target.checked,useIdForTa:!e.target.checked})}/>Use OD for t_a</label>
      <label><input type='checkbox' checked={noz.useIdForTa} onChange={e=>setNoz({...noz,useIdForTa:e.target.checked,useOdForTa:!e.target.checked})}/>Use ID for t_a</label>
      <button type='submit'>Calculate</button>
      {nozResult && <div>Formula: {fmt(nozResult.governingRequiredThicknessIn)} in; Required+CA: {fmt(nozResult.requiredThicknessPlusCorrosionAllowanceIn)} in; Margin: {fmt(nozResult.marginIn)} in; {nozResult.isAcceptable?'Acceptable':'Not acceptable'} {nozResult.errorMessage && <div>{nozResult.errorMessage}</div>} {nozResult.warnings.map(w=><div key={w}>{w}</div>)}</div>}
    </form>}
  </div>;
}
