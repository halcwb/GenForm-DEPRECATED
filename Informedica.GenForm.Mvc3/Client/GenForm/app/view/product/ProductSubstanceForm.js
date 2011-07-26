Ext.define('GenForm.view.product.ProductSubstanceForm', {
    extend: 'GenForm.lib.view.form.FormBase',
    alias: 'widget.productsubstanceform',

    title:'Stoffen',

    createItems: function () {
        var me = this;
        return [
            {
                defaults: {width: 230},
                defaultType: 'textfield',
                padding: '10x',

                items: [
                    {
                        fieldLabel: 'Volgorde',
                        name: 'OrderNumber',
                        allowBlank:true
                    },
                    me.createSubstanceCombo(),
                    {
                        fieldLabel: 'Hoeveelheid',
                        name: 'Quantity'
                    },
                    me.createUnitCombo()
                ]
            }
        ];
    },

    createSubstanceCombo: function () {
        var me = this, combo;

        combo = me.createEditCombo({
            name: 'SubstanceName',
            fieldLabel: 'Stof',
            margin: '10 10 10 10',
            displayField: 'ProductSubstance',
            store: 'product.SubstanceName'
        });

        return combo;

    },

    createUnitCombo: function () {
        var me = this, combo;

        combo = me.createEditCombo({
            name: 'UnitName',
            fieldLabel: 'Eenheid',
            margin: '10 10 10 10',
            displayField: 'UnitName',
            store: 'product.UnitName'
        });
        
        return combo;
    }

});