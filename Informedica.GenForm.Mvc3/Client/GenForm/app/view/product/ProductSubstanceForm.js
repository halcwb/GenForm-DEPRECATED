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
                    me.createOrderNumberField(),
                    me.createSubstanceCombo(),
                    me.createQuantityField(),
                    me.createUnitCombo()
                ]
            }
        ];
    },

    createQuantityField: function () {
        var me = this, config;

        config = {
            fieldLabel: 'Hoeveelheid',
            name: 'Quantity'
        };
        return me.createNumberField(config);
    },

    createOrderNumberField: function () {
        var me = this, config;

        config = {
            fieldLabel: 'Volgorde',
            name: 'OrderNumber',
            allowBlank:true
        };

        return me.createNumberField(config);
    },

    createSubstanceCombo: function () {
        var me = this, config;

        config = {
            name: 'SubstanceName',
            fieldLabel: 'Stof',
            margin: '10 10 10 10',
            displayField: 'SubstanceName',
            store: 'product.SubstanceName'
        };

        return me.createComboBox(config);
    },

    createUnitCombo: function () {
        var me = this, config;

        config = ({
            name: 'UnitName',
            fieldLabel: 'Eenheid',
            margin: '10 10 10 10',
            displayField: 'UnitName',
            store: 'product.UnitName'
        });

        return me.createComboBox(config);
    }

});