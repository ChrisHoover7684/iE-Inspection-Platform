import { getMvpExternalInspectionFields } from './inspectionFieldCatalog';

export const FIELD_CATALOG_WORD_TITLE = 'External Inspection Field Catalog Review';
export const FIELD_CATALOG_WORD_INTRO = 'This document is a field catalog review document. It is not a final report template. Use it to review field names, tags, grouping, options, and layout order before generating report templates.';

const esc = (v: string) => v.replace(/&/g, '&amp;').replace(/</g, '&lt;').replace(/>/g, '&gt;');
const groupKey = (f: ReturnType<typeof getMvpExternalInspectionFields>[number]) => `${f.standard} | ${f.inspectionScope} | ${f.equipmentFamily} | ${f.equipmentSubtype} | ${f.sectionGroup} | ${f.componentType}`;

const textCell = (text: string) => `<w:tc><w:p><w:r><w:t xml:space="preserve">${esc(text)}</w:t></w:r></w:p></w:tc>`;

const buildDocumentXml = () => {
  const fields = [...getMvpExternalInspectionFields()].sort((a, b) => groupKey(a).localeCompare(groupKey(b)) || a.defaultLayoutOrder - b.defaultLayoutOrder || a.fieldTag.localeCompare(b.fieldTag));
  const headers = ['Field Tag', 'Label', 'Section Group', 'Component Type', 'Data Type', 'Options', 'Required', 'Supports Finding', 'Supports Recommendation', 'Supports Repair Required', 'Supports Photo Tag', 'Supports Summary', 'Supports NDE Request', 'Default Layout Order', 'Review Notes'];
  const headerRow = `<w:tr>${headers.map((h) => textCell(h)).join('')}</w:tr>`;
  const rows = fields.map((f) => `<w:tr>${[
    f.fieldTag, f.label, f.sectionGroup, f.componentType, f.dataType, f.options.join(', '), String(f.required), String(f.supportsFinding), String(f.supportsRecommendation), String(f.supportsRepairRequired), String(f.supportsPhotoTag), String(f.supportsSummary), String(f.supportsNdeRequest), String(f.defaultLayoutOrder), f.reviewNotes ?? ''
  ].map(textCell).join('')}</w:tr>`).join('');

  return `<?xml version="1.0" encoding="UTF-8" standalone="yes"?>
<w:document xmlns:w="http://schemas.openxmlformats.org/wordprocessingml/2006/main"><w:body>
<w:p><w:r><w:t>${esc(FIELD_CATALOG_WORD_TITLE)}</w:t></w:r></w:p>
<w:p><w:r><w:t xml:space="preserve">${esc(FIELD_CATALOG_WORD_INTRO)}</w:t></w:r></w:p>
<w:tbl>${headerRow}${rows}</w:tbl>
<w:sectPr/></w:body></w:document>`;
};

export const buildFieldCatalogWordReviewDocument = () => buildDocumentXml();

// minimal zip (store method)
const crcTable = new Uint32Array(256).map((_, n) => { let c = n; for (let k = 0; k < 8; k++) c = (c & 1) ? (0xedb88320 ^ (c >>> 1)) : (c >>> 1); return c >>> 0; });
const crc32 = (data: Uint8Array) => { let c = 0xffffffff; for (const b of data) c = crcTable[(c ^ b) & 0xff] ^ (c >>> 8); return (c ^ 0xffffffff) >>> 0; };
const u16 = (n: number) => [n & 255, (n >>> 8) & 255]; const u32 = (n: number) => [n & 255, (n >>> 8) & 255, (n >>> 16) & 255, (n >>> 24) & 255];

function zipStore(files: { name: string; data: Uint8Array }[]): Uint8Array {
  const out: number[] = []; const central: number[] = []; let offset = 0;
  for (const f of files) {
    const name = new TextEncoder().encode(f.name); const crc = crc32(f.data);
    const local = [0x50,0x4b,0x03,0x04,...u16(20),...u16(0),...u16(0),...u16(0),...u16(0),...u32(crc),...u32(f.data.length),...u32(f.data.length),...u16(name.length),...u16(0),...name,...f.data];
    out.push(...local);
    const cdir = [0x50,0x4b,0x01,0x02,...u16(20),...u16(20),...u16(0),...u16(0),...u16(0),...u16(0),...u32(crc),...u32(f.data.length),...u32(f.data.length),...u16(name.length),...u16(0),...u16(0),...u16(0),...u16(0),...u32(0),...u32(offset),...name];
    central.push(...cdir); offset += local.length;
  }
  const centralOffset = out.length; out.push(...central);
  out.push(0x50,0x4b,0x05,0x06,...u16(0),...u16(0),...u16(files.length),...u16(files.length),...u32(central.length),...u32(centralOffset),...u16(0));
  return new Uint8Array(out);
}

export async function exportFieldCatalogWordReview(filename = 'external-inspection-field-catalog-review.docx'): Promise<Blob> {
  void filename;
  const files = [
    { name: '[Content_Types].xml', data: new TextEncoder().encode('<?xml version="1.0" encoding="UTF-8"?><Types xmlns="http://schemas.openxmlformats.org/package/2006/content-types"><Default Extension="rels" ContentType="application/vnd.openxmlformats-package.relationships+xml"/><Default Extension="xml" ContentType="application/xml"/><Override PartName="/word/document.xml" ContentType="application/vnd.openxmlformats-officedocument.wordprocessingml.document.main+xml"/></Types>') },
    { name: '_rels/.rels', data: new TextEncoder().encode('<?xml version="1.0" encoding="UTF-8"?><Relationships xmlns="http://schemas.openxmlformats.org/package/2006/relationships"><Relationship Id="rId1" Type="http://schemas.openxmlformats.org/officeDocument/2006/relationships/officeDocument" Target="word/document.xml"/></Relationships>') },
    { name: 'word/document.xml', data: new TextEncoder().encode(buildDocumentXml()) }
  ];
  const zip = zipStore(files);
  const bytes = Array.from(zip);
  return new Blob([new Uint8Array(bytes)], { type: 'application/vnd.openxmlformats-officedocument.wordprocessingml.document' });
}
