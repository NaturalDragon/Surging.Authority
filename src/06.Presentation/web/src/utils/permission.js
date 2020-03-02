const permissionJson = {
    module: {
        new: 'api/module/Create',
        modify:'api/module/Modify',
        list: 'api/module/GetPageList',
        detail:'/api/module/GetForModify',
        remove:'api/module/Remove'
        
    },
    menu: {
        new: 'api/menu/Create',
        list: 'api/menu/GetMenuTree',
        detail:'api/menu/GetForModify',
        modify:'api/menu/Modify',
        remove:'api/menu/Remove'
    },
    structure:{
        orgTree:'api/Organization/GetOrganizationByParentId',
        orgNew:'api/Organization/Create',
        orgDetail:'api/Organization/GetForModify',
        orgModify:'api/Organization/Modify',
        orgRemove:'api/Organization/Remove',
        roleTree:'api/role/GetRoleTypeList',
        roleNew:'api/role/Create',
        roleDetail:'api/role/GetForModify',
        roleModify:'api/role/Modify',
        roleRemove:'api/role/Remove',
        empList:'api/Employee/GetListPagedByOrgIdOrRoleId',
        empNew:'api/Employee/Create',
        empDetail:'api/Employee/GetForModify',
        empModify:'api/Employee/Modify',
        empRemove:'api/Employee/Remove'
    },
    power:{
        getOrgMenu:'api/Menu/GetOrgMenu',
        getRoleMenu:'api/Menu/GetRoleMenu',
        getEmployeeMenu:'api/Menu/GetEmployeeMenu',
        empList:'api/Employee/GetListPagedOriginal',
        saveOrgMenu:'api/Menu/SaveOrgMenu',
        saveRoleMenu:'api/Menu/SaveRoleMenu',
        saveEmployeeMenu:'api/Menu/SaveEmployeeMenu',
       // saveElementPower:'/Permission/api/ElementPower/SaveElementPower',
        getAllModule:'api/Module/GetModulesWithElements',
        getElementPower:'/Permission/api/ElementPower/GetElementPower',
       // GgtEmployeeElementPower:'/Permission/api/ElementPower/GetEmployeeElementPower',
       getOrgModule:'api/ModuleElement/GetOrganizationModuleElement',
       getRoleModule:'api/ModuleElement/GetRoleModuleElement',
       getEmpModule:'api/ModuleElement/GetEmployeeModuleElement',
       saveOrgModule:'api/ModuleElement/SaveOrganizationModuleElement',
       saveRoleModule:'api/ModuleElement/SaveRoleModuleElement',
       saveEmpModule:'api/ModuleElement/SaveEmployeeModuleElement'
    }
  
}

module.exports = {
    permissionJson
}