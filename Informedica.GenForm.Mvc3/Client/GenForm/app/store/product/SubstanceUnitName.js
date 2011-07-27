Ext.define('GenForm.store.product.SubstanceUnitName', {
    extend: 'Ext.data.Store',

    model: 'GenForm.model.product.UnitName',

    // ToDo implement serverside method
    proxy: {
        type: 'direct',
        directFn: Tests.GetSubstanceUnitNames
    }

});