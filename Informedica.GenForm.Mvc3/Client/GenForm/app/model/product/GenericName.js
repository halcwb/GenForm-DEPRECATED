Ext.define('GenForm.model.product.GenericName', {
    extend: 'Ext.data.Model',
    alias: 'widget.genericnamemodel',

    fields: [ {name: 'GenericName', type: 'string' }],

    proxy: {
        type: 'direct',
        directFn: Product.GetGenericNames
    }

});