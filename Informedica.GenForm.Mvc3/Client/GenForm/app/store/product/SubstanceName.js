Ext.define('GenForm.store.product.SubstanceName', {
    extend: 'Ext.data.Store',
    alias: 'widget.substancenamestore',
    storeId: 'substancenamestore',
    
    model: 'GenForm.model.product.SubstanceName',
    autoLoad: true,

    proxy: {
        type: 'direct',
        directFn: Tests.GetSubstanceNames
    }

});