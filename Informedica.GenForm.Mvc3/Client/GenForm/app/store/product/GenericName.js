Ext.define('GenForm.store.product.GenericName', {
    extend: 'Ext.data.Store',
    alias: 'widget.genericnamestore',
    storeId: 'genericnamestore',
    
    model: 'GenForm.model.product.GenericName',
    autoLoad: true
});