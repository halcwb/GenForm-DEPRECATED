Ext.define('GenForm.model.product.SubstanceName', {
    extend: 'Ext.data.Model',
    alias: 'widget.substancenamemodel',

    fields: [
        {name: 'SubstanceName', type: 'string', mapping: 'SubstanceName'}
    ],

    // ToDo: Implement server side method Product.GetSubstanceNames
    proxy: {
        type: 'direct', 
        directFn: Tests.GetSubstanceNames
    }

});