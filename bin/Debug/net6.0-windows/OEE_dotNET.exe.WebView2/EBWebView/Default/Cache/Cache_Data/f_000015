"use strict";(self.webpackChunkgrafana=self.webpackChunkgrafana||[]).push([[9427],{29325:(A,M,t)=>{t.d(M,{j:()=>d});var n=t(42844),e=t(27702),l=t(34667),g=t(66310),O=t(66670),P=t(28580),y=t(73453),c=t(17112);const d=({onChange:p,maxMenuHeight:u,isWidget:s=!1})=>{const C=()=>l.config.featureToggles.vizAndWidgetSplit?s?(0,c.Ry)():(0,c.hK)():(0,c.xW)(),r=(0,e.useMemo)(C,[s]),I=(0,e.useMemo)(()=>r.map(o=>({label:o.name,imgUrl:o.info.logos.small,value:o})).sort((o,D)=>o.label?.localeCompare(D.label)),[r]),[v,b]=(0,e.useState)([]),f=(0,e.useCallback)(o=>{const D=o.filter(W=>W.value).map(W=>W.value);p(D),b(o)},[p]),L=(0,g.wW)(E),R={defaultOptions:!0,getOptionLabel:o=>o.label,getOptionValue:o=>o.value,noOptionsMessage:"No Panel types found",placeholder:"Filter by type",maxMenuHeight:u,options:I,value:v,onChange:f};return e.createElement("div",{className:L.container},v.length>0&&e.createElement(O.zx,{size:"xs",icon:"trash-alt",fill:"text",className:L.clear,onClick:()=>f([]),"aria-label":"Clear types"},"Clear types"),e.createElement(P.NU,{...R,prefix:e.createElement(y.J,{name:"filter"}),"aria-label":"Panel Type filter"}))};function E(p){return{container:(0,n.css)`
      label: container;
      position: relative;
      min-width: 180px;
      flex-grow: 1;
    `,clear:(0,n.css)`
      label: clear;
      font-size: ${p.spacing(1.5)};
      position: absolute;
      top: -${p.spacing(4.5)};
      right: 0;
    `}}},5336:(A,M,t)=>{t.d(M,{P:()=>c});var n=t(27702),e=t(25111),l=t(28580),g=t(73453),O=t(15758),P=t(63064);const y=()=>(0,P.getGrafanaSearcher)().getSortOptions();function c({onChange:d,value:E,placeholder:p,filter:u,getSortOptions:s,isClearable:C}){const r=(0,e.Z)(async()=>{const v=await(s??y)();return u?v.filter(b=>u.includes(b.value)):v},[s,u]);if(r.loading)return null;const I=!!(E?.includes("desc")||E?.startsWith("-"));return n.createElement(l.Ph,{key:E,width:28,onChange:d,value:r.value?.find(v=>v.value===E)??null,options:r.value,"aria-label":"Sort",placeholder:p??`Sort (Default ${O.yw.label})`,prefix:n.createElement(g.J,{name:I?"sort-amount-down":"sort-amount-up"}),isClearable:C})}},56430:(A,M,t)=>{t.d(M,{p:()=>W});var n=t(42844),e=t(27702),l=t(34667),g=t(66310),O=t(73453),P=t(9848),y=t(59307),c=t(56983),d=t(53933),E=t(13741),p=t(66670),u=t(51214),s=t(18167),C=t(75766),r=t(34099);const I={loadingState:d.Gu.Loading,dashboardTitles:[]},v=(0,r.PH)("libraryPanels/delete/searchCompleted"),b=(a=I,i)=>v.match(i)?{...a,dashboardTitles:i.payload.dashboards.map(m=>m.title),loadingState:d.Gu.Done}:a;function f(a){return async function(i){const m=await(0,C.E8)(a.uid);i(v({dashboards:m}))}}const L=({libraryPanel:a,onDismiss:i,onConfirm:m})=>{const x=(0,g.wW)(u.J),[{dashboardTitles:_,loadingState:h},B]=(0,e.useReducer)(b,I),U=(0,e.useMemo)(()=>(0,s.tb)(B),[B]);(0,e.useEffect)(()=>{U(f(a))},[U,a]);const $=!!_.length,S=h===d.Gu.Done;return e.createElement(E.u,{className:x.modal,title:"Delete library panel",icon:"trash-alt",onDismiss:i,isOpen:!0},S?null:e.createElement(R,null),S?e.createElement("div",null,$?e.createElement(D,{dashboardTitles:_}):null,$?null:e.createElement(o,null),e.createElement(E.u.ButtonRow,null,e.createElement(p.zx,{variant:"secondary",onClick:i,fill:"outline"},"Cancel"),e.createElement(p.zx,{variant:"destructive",onClick:m,disabled:$},"Delete"))):null)},R=()=>e.createElement("span",null,"Loading library panel..."),o=()=>{const a=(0,g.wW)(u.J);return e.createElement("div",{className:a.modalText},"Do you want to delete this panel?")},D=({dashboardTitles:a})=>{const i=(0,g.wW)(u.J),m=a.length===1?"dashboard.":"dashboards.",x=`${a.length} ${m}`;return a.length===0?null:e.createElement("div",null,e.createElement("p",{className:i.textInfo},"This library panel can not be deleted because it is connected to ",e.createElement("strong",null,x)," Remove the library panel from the dashboards listed below and retry."),e.createElement("table",{className:i.myTable},e.createElement("thead",null,e.createElement("tr",null,e.createElement("th",null,"Dashboard name"))),e.createElement("tbody",null,a.map((_,h)=>e.createElement("tr",{key:`dash-title-${h}`},e.createElement("td",null,_))))))},W=({libraryPanel:a,onClick:i,onDelete:m,showSecondaryActions:x})=>{const[_,h]=(0,e.useState)(!1),B=()=>{m?.(a),h(!1)},U=l.config.panels[a.model.type]??(0,y.X)(a.model.type).meta;return e.createElement(e.Fragment,null,e.createElement(c.X,{isCurrent:!1,title:a.name,description:a.description,plugin:U,onClick:()=>i?.(a),onDelete:x?()=>h(!0):void 0},e.createElement(T,{libraryPanel:a})),_&&e.createElement(L,{libraryPanel:a,onConfirm:B,onDismiss:()=>h(!1)}))};function T({libraryPanel:a}){const i=(0,g.wW)(K);return!a.meta?.folderUid&&!a.meta?.folderName?null:a.meta.folderUid?e.createElement("span",{className:i.metaContainer},e.createElement(P.r,{href:`/dashboards/f/${a.meta.folderUid}`},e.createElement(O.J,{name:"folder-upload",size:"sm"}),e.createElement("span",null,a.meta.folderName))):e.createElement("span",{className:i.metaContainer},e.createElement(O.J,{name:"folder",size:"sm"}),e.createElement("span",null,a.meta.folderName))}function K(a){return{metaContainer:(0,n.css)`
      display: flex;
      align-items: center;
      color: ${a.colors.text.secondary};
      font-size: ${a.typography.bodySmall.fontSize};
      padding-top: ${a.spacing(.5)};

      svg {
        margin-right: ${a.spacing(.5)};
        margin-bottom: 3px;
      }
    `}}},29427:(A,M,t)=>{t.d(M,{N:()=>D,e:()=>o});var n=t(42844),e=t(27702),l=t(29956),g=t(66310),O=t(64022),P=t(33716),y=t(56027),c=t.n(y),d=t(66670),E=t(28580),p=t(73453),u=t(45937),s=t(14172),C=t(18771);function r({onChange:a,maxMenuHeight:i}){const m=(0,g.wW)(v),[x,_]=(0,e.useState)(!1),h=(0,e.useCallback)(N=>I(N,_),[]),B=(0,e.useMemo)(()=>c()(h,300),[h]),[U,$]=(0,e.useState)([]),S=(0,e.useCallback)(N=>{const z=N.filter(F=>!!F.value).map(F=>F.value);a(z),$(N)},[a]);return e.createElement("div",{className:m.container},U.length>0&&e.createElement(d.zx,{size:"xs",icon:"trash-alt",fill:"text",className:m.clear,onClick:()=>a([]),"aria-label":"Clear folders"},"Clear folders"),e.createElement(E.M8,{value:U,onChange:S,isLoading:x,loadOptions:B,maxMenuHeight:i,placeholder:"Filter by folder",noOptionsMessage:"No folders found",prefix:e.createElement(p.J,{name:"filter"}),"aria-label":"Folder filter",defaultOptions:!0}))}async function I(a,i){i(!0);const m={query:a,type:s.o.DashFolder,permission:C.PermissionLevelString.View},_=(await(0,u.i)().search(m)).map(h=>({label:h.title,value:{uid:h.uid,title:h.title}}));return(!a||"general".includes(a.toLowerCase()))&&_.unshift({label:"General",value:{uid:"general",title:"General"}}),i(!1),_}function v(a){return{container:(0,n.css)`
      label: container;
      position: relative;
      min-width: 180px;
      flex-grow: 1;
    `,clear:(0,n.css)`
      label: clear;
      font-size: ${a.spacing(1.5)};
      position: absolute;
      top: -${a.spacing(4.5)};
      right: 0;
    `}}var b=t(29325),f=t(5336),L=t(97957),R=t(83929),o=(a=>(a.Tight="tight",a.Spacious="spacious",a))(o||{});const D=({onClick:a,variant:i="spacious",currentPanelId:m,currentFolderUID:x,perPage:_=L.gN,showPanelFilter:h=!1,showFolderFilter:B=!1,showSort:U=!1,showSecondaryActions:$=!1})=>{const S=(0,g.wW)((0,e.useCallback)(Y=>W(Y,i),[i])),[N,z]=(0,e.useState)(""),[F,j]=(0,e.useState)("");(0,l.Z)(()=>j(N),200,[N]);const[V,G]=(0,e.useState)({}),[H,J]=(0,e.useState)(x?[x]:[]),[Q,w]=(0,e.useState)([]),X=U||h||B,Z=i==="tight"?"lg":"xs";return e.createElement("div",{className:S.container},e.createElement(O.wc,{spacing:Z},e.createElement("div",{className:S.gridContainer},e.createElement("div",{className:S.filterInputWrapper},e.createElement(P.H,{value:N,onChange:z,placeholder:"Search by name or description",width:0,escapeRegex:!1})),X&&e.createElement(T,{showSort:U,showPanelFilter:h,showFolderFilter:B,onSortChange:G,onFolderFilterChange:J,onPanelFilterChange:w,sortDirection:V.value,variant:i})),e.createElement("div",{className:S.libraryPanelsView},e.createElement(R.u,{onClickCard:a,searchString:F,sortDirection:V.value,panelFilter:Q,folderFilter:H,currentPanelId:m,showSecondaryActions:$,perPage:_}))))};function W(a,i){const m=(0,n.css)`
    flex-direction: row;
    row-gap: ${a.spacing(1)};
  `;return{filterInputWrapper:(0,n.css)`
      flex-grow: ${i==="tight"?1:"initial"};
    `,container:(0,n.css)`
      width: 100%;
      overflow-y: auto;
      padding: ${a.spacing(1)};
    `,libraryPanelsView:(0,n.css)`
      width: 100%;
    `,gridContainer:(0,n.css)`
      ${i==="tight"?m:""};
      display: flex;
      flex-direction: column;
      width: 100%;
      column-gap: ${a.spacing(1)};
      row-gap: ${a.spacing(1)};
      padding-bottom: ${a.spacing(2)};
    `}}const T=e.memo(({variant:a="spacious",showSort:i,showPanelFilter:m,showFolderFilter:x,sortDirection:_,onSortChange:h,onFolderFilterChange:B,onPanelFilterChange:U})=>{const $=(0,g.wW)((0,e.useCallback)(z=>K(z,a),[a])),S=(0,e.useCallback)(z=>U(z.map(F=>F.id)),[U]),N=(0,e.useCallback)(z=>B(z.map(F=>F.uid??"")),[B]);return e.createElement("div",{className:$.container},i&&e.createElement(f.P,{value:_,onChange:h,filter:["alpha-asc","alpha-desc"]}),(x||m)&&e.createElement("div",{className:$.filterContainer},x&&e.createElement(r,{onChange:N}),m&&e.createElement(b.j,{onChange:S})))});T.displayName="SearchControls";function K(a,i="spacious"){const m=(0,n.css)`
    display: flex;
    gap: ${a.spacing(1)};
    flex-grow: 1;
    flex-direction: row;
    justify-content: end;
  `,x=(0,n.css)`
    ${m};
    flex-grow: initial;
    flex-direction: column;
    justify-content: normal;
  `,_=(0,n.css)`
    display: flex;
    flex-direction: row;
    margin-left: auto;
    gap: 4px;
  `,h=(0,n.css)`
    ${_};
    flex-direction: column;
    margin-left: initial;
  `;switch(i){case"spacious":return{container:m,filterContainer:_};case"tight":return{container:x,filterContainer:h}}}},83929:(A,M,t)=>{t.d(M,{u:()=>E});var n=t(42844),e=t(27702),l=t(29956),g=t(53933),O=t(66310),P=t(50852),y=t(56430),c=t(18167),d=t(58300);const E=({className:u,onClickCard:s,searchString:C,sortDirection:r,panelFilter:I,folderFilter:v,showSecondaryActions:b,currentPanelId:f,perPage:L=40,isWidget:R})=>{const o=(0,O.wW)(p),[{libraryPanels:D,page:W,perPage:T,numberOfPages:K,loadingState:a,currentPanelId:i},m]=(0,e.useReducer)(d._p,{...d.p$,currentPanelId:f,perPage:L}),x=(0,e.useMemo)(()=>(0,c.tb)(m),[m]);(0,l.Z)(()=>x((0,c.Xu)({searchString:C,sortDirection:r,panelFilter:I,folderFilterUIDs:v,page:W,perPage:T,currentPanelId:i,isWidget:R})),300,[C,r,I,v,W,x]);const _=({uid:B})=>x((0,c.UO)(B,{searchString:C,sortDirection:r,panelFilter:I,folderFilterUIDs:v,page:W,perPage:T})),h=B=>x((0,d.oO)({page:B}));return e.createElement("div",{className:(0,n.cx)(o.container,u)},e.createElement("div",{className:o.libraryPanelList},a===g.Gu.Loading?e.createElement("p",null,"Loading library panels..."):D.length<1?e.createElement("p",{className:o.noPanelsFound},"No library panels found."):D?.map((B,U)=>e.createElement(y.p,{key:`library-panel=${U}`,libraryPanel:B,onDelete:_,onClick:s,showSecondaryActions:b}))),D.length?e.createElement("div",{className:o.pagination},e.createElement(P.t,{currentPage:W,numberOfPages:K,onNavigate:h,hideWhenSinglePage:!0})):null)},p=u=>({container:(0,n.css)`
      display: flex;
      flex-direction: column;
      flex-wrap: nowrap;
    `,libraryPanelList:(0,n.css)`
      max-width: 100%;
      display: grid;
      grid-gap: ${u.spacing(1)};
    `,searchHeader:(0,n.css)`
      display: flex;
    `,newPanelButton:(0,n.css)`
      margin-top: 10px;
      align-self: flex-start;
    `,pagination:(0,n.css)`
      align-self: center;
      margin-top: ${u.spacing(1)};
    `,noPanelsFound:(0,n.css)`
      label: noPanelsFound;
      min-height: 200px;
    `})},18167:(A,M,t)=>{t.d(M,{UO:()=>v,Xu:()=>I,tb:()=>b});var n=t(19973),e=t(91053),l=t(45383),g=t(6857),O=t(80852),P=t(22479),y=t(56469),c=t(90581),d=t(32900),E=t(58885),p=t(24321),u=t(34667),s=t(17112),C=t(75766),r=t(58300);function I(f){const L=(o,D)=>o.id===D.type,R=(o,D)=>{const W=(0,s.xW)();return o.filter(T=>{const K=W.find(a=>L(a,T));return D?!!K?.skipDataQuery:!K?.skipDataQuery})};return function(o){const D=new n.w0,W=(0,e.D)((0,C.Pq)({searchString:f.searchString,perPage:f.perPage,page:f.page,excludeUid:f.currentPanelId,sortDirection:f.sortDirection,typeFilter:f.panelFilter,folderFilterUIDs:f.folderFilterUIDs})).pipe((0,P.z)(T=>{const{elements:K}=T;if(u.config.featureToggles.vizAndWidgetSplit&&f.isWidget!==void 0){const a=R(K,f.isWidget);return(0,l.of)({...T,elements:a})}return(0,l.of)({...T,elements:K})}),(0,P.z)(({perPage:T,elements:K,page:a,totalCount:i})=>(0,l.of)((0,r.zK)({libraryPanels:K,page:a,perPage:T,totalCount:i}))),(0,y.K)(T=>(console.error(T),(0,l.of)((0,r.zK)({...r.p$,page:f.page,perPage:f.perPage})))),(0,c.x)(()=>D.unsubscribe()),(0,d.B)());D.add((0,g.T)((0,O.H)(50).pipe((0,E.h)((0,r.xU)()),(0,p.R)(W)),W).subscribe(o))}}function v(f,L){return async function(R){try{await(0,C.UO)(f),I(L)(R)}catch(o){console.error(o)}}}function b(f){return function(L){return L instanceof Function?L(f):f(L)}}},58300:(A,M,t)=>{t.d(M,{_p:()=>y,oO:()=>P,p$:()=>l,xU:()=>g,zK:()=>O});var n=t(34099),e=t(53933);const l={loadingState:e.Gu.Loading,libraryPanels:[],totalCount:0,perPage:40,page:1,numberOfPages:0,currentPanelId:void 0},g=(0,n.PH)("libraryPanels/view/initSearch"),O=(0,n.PH)("libraryPanels/view/searchCompleted"),P=(0,n.PH)("libraryPanels/view/changePage"),y=(c,d)=>{if(g.match(d))return{...c,loadingState:e.Gu.Loading};if(O.match(d)){const{libraryPanels:E,page:p,perPage:u,totalCount:s}=d.payload,C=Math.ceil(s/u);return{...c,libraryPanels:E,perPage:u,totalCount:s,loadingState:e.Gu.Done,numberOfPages:C,page:p>C?p-1:p}}return P.match(d)?{...c,page:d.payload.page}:c}},51214:(A,M,t)=>{t.d(M,{J:()=>e});var n=t(42844);function e(l){return{myTable:(0,n.css)`
      max-height: 204px;
      overflow-y: auto;
      margin-top: 11px;
      margin-bottom: 28px;
      border-radius: ${l.shape.borderRadius(1)};
      border: 1px solid ${l.colors.action.hover};
      background: ${l.colors.background.primary};
      color: ${l.colors.text.secondary};
      font-size: ${l.typography.h6.fontSize};
      width: 100%;

      thead {
        color: #538ade;
        font-size: ${l.typography.bodySmall.fontSize};
      }

      th,
      td {
        padding: 6px 13px;
        height: ${l.spacing(4)};
      }

      tbody > tr:nth-child(odd) {
        background: ${l.colors.background.secondary};
      }
    `,noteTextbox:(0,n.css)`
      margin-bottom: ${l.spacing(4)};
    `,textInfo:(0,n.css)`
      color: ${l.colors.text.secondary};
      font-size: ${l.typography.size.sm};
    `,dashboardSearch:(0,n.css)`
      margin-top: ${l.spacing(2)};
    `,modal:(0,n.css)`
      width: 500px;
    `,modalText:(0,n.css)`
      font-size: ${l.typography.h4.fontSize};
      color: ${l.colors.text.primary};
      margin-bottom: ${l.spacing(4)};
      padding-top: ${l.spacing(2)};
    `}}},56983:(A,M,t)=>{t.d(M,{X:()=>E});var n=t(42844),e=t(27702),l=t(9232),g=t(81538),O=t(74639),P=t(66310),y=t(67437),c=t(97918),d=t(90741);const E=({isCurrent:s,title:C,plugin:r,onClick:I,onDelete:v,disabled:b,showBadge:f,description:L,children:R})=>{const o=(0,P.wW)(p),D=b||r.state===l.BV.deprecated,W=(0,n.cx)({[o.item]:!0,[o.itemDisabled]:D,[o.current]:s});return e.createElement("div",{className:W,"aria-label":O.wl.components.PluginVisualization.item(r.name),"data-testid":O.wl.components.PluginVisualization.item(r.name),onClick:D?void 0:I,title:s?"Click again to close this section":r.name},e.createElement("img",{className:(0,n.cx)(o.img,{[o.disabled]:D}),src:r.info.logos.small,alt:""}),e.createElement("div",{className:(0,n.cx)(o.itemContent,{[o.disabled]:D})},e.createElement("div",{className:o.name},C),L?e.createElement("span",{className:o.description},L):null,R),f&&e.createElement("div",{className:(0,n.cx)(o.badge,{[o.disabled]:D})},e.createElement(u,{plugin:r})),v&&e.createElement(y.h,{name:"trash-alt",onClick:T=>{T.stopPropagation(),v()},className:o.deleteButton,"aria-label":"Delete button on panel type card",tooltip:"Delete"}))};E.displayName="PanelTypeCard";const p=s=>({item:(0,n.css)`
      position: relative;
      display: flex;
      flex-shrink: 0;
      cursor: pointer;
      background: ${s.colors.background.secondary};
      border-radius: ${s.shape.borderRadius()};
      box-shadow: ${s.shadows.z1};
      border: 1px solid ${s.colors.background.secondary};
      align-items: center;
      padding: 8px;
      width: 100%;
      position: relative;
      overflow: hidden;
      transition: ${s.transitions.create(["background"],{duration:s.transitions.duration.short})};

      &:hover {
        background: ${s.colors.emphasize(s.colors.background.secondary,.03)};
      }
    `,itemContent:(0,n.css)`
      overflow: hidden;
      position: relative;
      padding: ${s.spacing(0,1)};
    `,itemDisabled:(0,n.css)`
      cursor: default;

      &,
      &:hover {
        background: ${s.colors.action.disabledBackground};
      }
    `,current:(0,n.css)`
      label: currentVisualizationItem;
      border: 1px solid ${s.colors.primary.border};
      background: ${s.colors.action.selected};
    `,disabled:(0,n.css)`
      opacity: 0.6;
      filter: grayscale(1);
      cursor: default;
      pointer-events: none;
    `,name:(0,n.css)`
      text-overflow: ellipsis;
      overflow: hidden;
      font-size: ${s.typography.size.sm};
      font-weight: ${s.typography.fontWeightMedium};
      width: 100%;
    `,description:(0,n.css)`
      display: block;
      text-overflow: ellipsis;
      overflow: hidden;
      color: ${s.colors.text.secondary};
      font-size: ${s.typography.bodySmall.fontSize};
      font-weight: ${s.typography.fontWeightLight};
      width: 100%;
      max-height: 4.5em;
    `,img:(0,n.css)`
      max-height: 38px;
      width: 38px;
      display: flex;
      align-items: center;
    `,badge:(0,n.css)`
      background: ${s.colors.background.primary};
    `,deleteButton:(0,n.css)`
      cursor: pointer;
      margin-left: auto;
    `}),u=({plugin:s})=>(0,g.x)(s.signature)?e.createElement(c.o,{status:s.signature}):e.createElement(d.u,{state:s.state});u.displayName="PanelPluginBadge"},17112:(A,M,t)=>{t.d(M,{Ry:()=>O,hK:()=>P,rw:()=>y,xW:()=>g});var n=t(9232),e=t(95676),l=t(12308);function g(){const c=l.config.panels;return Object.keys(c).filter(d=>c[d].hideFromList===!1).map(d=>c[d]).sort((d,E)=>d.sort-E.sort)}function O(){return g().filter(c=>!!c.skipDataQuery)}function P(){return g().filter(c=>!c.skipDataQuery)}function y(c,d,E){if(!d.length)return c.filter(r=>r.state===n.BV.deprecated?E?.id===r.id:!0);const p=(0,e.x6)(d).toLowerCase(),u=[],s=[],C="graph".startsWith(p);for(const r of c){if(r.state===n.BV.deprecated&&E?.id!==r.id)continue;const v=r.name.toLowerCase().indexOf(p);v===0?u.push(r):v>0?s.push(r):C&&r.id==="timeseries"&&u.push(r)}return u.concat(s)}},90741:(A,M,t)=>{t.d(M,{u:()=>g});var n=t(27702),e=t(9232),l=t(25357);const g=P=>{const y=O(P.state);return y?n.createElement(l.C,{className:P.className,color:y.color,title:y.tooltip,text:y.text,icon:y.icon}):null};function O(P){switch(P){case e.BV.deprecated:return{text:"Deprecated",color:"red",tooltip:"This feature is deprecated and will be removed in a future release"};case e.BV.alpha:return{text:"Alpha",color:"blue",tooltip:"This feature is experimental and future updates might not be backward compatible"};case e.BV.beta:return{text:"Beta",color:"blue",tooltip:"This feature is close to complete but not fully tested"};default:return null}}}}]);

//# sourceMappingURL=9427.1e65164569048326187c.js.map