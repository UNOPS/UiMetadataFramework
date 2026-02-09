# Product/Service Schema Unification - Implementation Checklist

**Status:** ⚠️ NOT READY TO START - Preconditions must be completed first

**Last Updated:** 2026-02-09

---

## Phase 0: Readiness Assessment (MUST COMPLETE FIRST)

### 1. Full Dependency Audit
- [ ] Identify all code files that reference Product table
- [ ] Find all queries assuming physical product attributes  
- [ ] Map all stored procedures using Product
- [ ] Document all external integrations consuming Product data
- [ ] Identify reporting dependencies
- [ ] **Deliverable:** Dependency map with estimated change effort per component
- [ ] **Outcome:** Total files impacted: _______

### 2. Service Requirements Definition
- [ ] Confirm service pricing model (recurring vs one-time)
- [ ] Define service fulfillment workflow
- [ ] Clarify refund/return policies for services
- [ ] Determine tax calculation approach
- [ ] **Deliverable:** Service product requirements document

### 3. Checkout Behavior Specification
- [ ] Confirm mixed cart requirements (physical + service together?)
- [ ] Define checkout flow modifications
- [ ] Specify payment processing differences
- [ ] Clarify order confirmation and fulfillment separation
- [ ] **Deliverable:** Checkout flow specification document

### 4. Production Database Migration Test
- [ ] Create production database copy
- [ ] Execute migration scripts on copy
- [ ] Measure migration duration: _______
- [ ] Validate data integrity
- [ ] Test rollback procedures
- [ ] **Deliverable:** Migration test results and performance data

### 5. Feature Flag Framework Confirmation
- [ ] Verify feature flag system exists and is production-ready
- [ ] Define flag strategy for gradual rollout
- [ ] Document flag removal plan
- [ ] Test flag toggle in staging
- [ ] **Deliverable:** Feature flag implementation plan

### 6. Risk Mitigation Plan
- [ ] Define success metrics
- [ ] Establish monitoring and alerting
- [ ] Create detailed rollback runbook
- [ ] Plan user communication strategy
- [ ] **Deliverable:** Risk register and mitigation playbook

---

## Go/No-Go Decision Point

**Date:** _____________

### GO Criteria (ALL must be met):
- [ ] All 6 preconditions completed
- [ ] Dependency audit shows <500 files impacted
- [ ] Migration test completes in <4 hours
- [ ] Stakeholder alignment on requirements
- [ ] Engineering team has capacity for 4-8 week project
- [ ] Feature flags tested and validated

### Decision: 
- [ ] **GO** - Proceed with implementation
- [ ] **NO-GO** - Re-evaluate approach or choose Plan B
- [ ] **DEFER** - Address gaps and reassess

**Decision Maker Approval:**
- [ ] Engineering Lead: _________________ Date: _______
- [ ] System Architect: _________________ Date: _______
- [ ] Product Owner: ___________________ Date: _______

---

## Phase 1: Schema & Infrastructure (1-2 weeks)

**Start Date:** _______ **Target Completion:** _______

- [ ] Add `ProductType` column to Product table (default = 1)
- [ ] Add CHECK constraints for physical-only fields
- [ ] Create `ServiceToProductMapping` table
- [ ] Create data validation scripts
- [ ] Set up feature flags in code
- [ ] Configure monitoring and alerting
- [ ] Create rollback runbook
- [ ] Test schema changes in staging

---

## Phase 2: Data Migration (Week 2-3)

**Start Date:** _______ **Target Completion:** _______

### Migration Execution
- [ ] Backup production database
- [ ] Run migration in batches (1000 rows at a time)
- [ ] Populate ServiceToProductMapping table
- [ ] Validate data integrity
- [ ] Run post-migration validation queries
- [ ] Monitor error logs

### Validation Checks
- [ ] Physical products count matches: Expected ______ Actual ______
- [ ] Service products migrated: Expected ______ Actual ______
- [ ] No NULL values in required physical fields
- [ ] All mapping records created
- [ ] No orphaned records

---

## Phase 3: Core Implementation (2-4 weeks)

**Start Date:** _______ **Target Completion:** _______

### Cart Logic
- [ ] Remove physical-only validation
- [ ] Handle service items without shipping attributes
- [ ] Update cart line item display logic
- [ ] Modify cart totals calculation
- [ ] Unit tests updated
- [ ] Integration tests passing

### Order Processing
- [ ] Separate fulfillment flows for physical vs service
- [ ] Update order status transitions
- [ ] Modify fulfillment logic for non-shippable items
- [ ] Update order confirmation emails
- [ ] Unit tests updated
- [ ] Integration tests passing

### Shipping & Fulfillment
- [ ] Skip shipping calculations for service items
- [ ] Filter service items from pick lists
- [ ] Update warehouse management system integration
- [ ] Modify shipping label generation
- [ ] Unit tests updated
- [ ] Integration tests passing

### Tax Calculation
- [ ] Implement service-specific tax rules
- [ ] Handle mixed physical/service tax calculation
- [ ] Update tax reporting
- [ ] Unit tests updated
- [ ] Integration tests passing

### Inventory Management
- [ ] Exclude services from inventory tracking
- [ ] Update stock level checks
- [ ] Modify reorder point calculations
- [ ] Unit tests updated
- [ ] Integration tests passing

### Admin Interfaces
- [ ] Conditional form rendering based on product type
- [ ] Update validation rules
- [ ] Modify bulk import/export
- [ ] Update product creation workflows
- [ ] UI tests updated

---

## Phase 4: Supporting Systems (1-2 weeks)

**Start Date:** _______ **Target Completion:** _______

### Reporting & Analytics
- [ ] Separate revenue by product type
- [ ] Update KPI dashboards
- [ ] Modify sales reports
- [ ] Update margin analysis reports
- [ ] Verify report accuracy

### Search & Catalog
- [ ] Update search indexing
- [ ] Modify faceted navigation
- [ ] Update product detail page templates
- [ ] Adjust search ranking algorithms
- [ ] Re-index all products

### Integrations
- [ ] Update ERP integration
- [ ] Modify accounting system exports
- [ ] Update marketplace integrations
- [ ] Adjust CRM data synchronization
- [ ] Test all integration endpoints

### Documentation
- [ ] Update API documentation
- [ ] Update user guides
- [ ] Update admin documentation
- [ ] Update developer documentation

---

## Phase 5: Testing & Validation (1-2 weeks)

**Start Date:** _______ **Target Completion:** _______

### Testing
- [ ] Unit tests: ____% coverage
- [ ] Integration tests passing
- [ ] Performance tests: Load time < ___ ms
- [ ] Regression tests passing
- [ ] User acceptance testing completed
- [ ] Security review completed

### Performance Validation
- [ ] Query performance acceptable
- [ ] Page load times acceptable
- [ ] Database query optimization complete
- [ ] Index strategy validated

---

## Phase 6: Gradual Rollout (1-2 weeks)

**Start Date:** _______ **Target Completion:** _______

### Rollout Steps
- [ ] Deploy to production with feature flag OFF
- [ ] Enable for 5% of users
  - Date: _______
  - Monitor for 2-3 days
  - Error rate: ______%
  - Performance impact: ______%
- [ ] Expand to 25% of users
  - Date: _______
  - Monitor for 2-3 days
  - Error rate: ______%
  - Performance impact: ______%
- [ ] Expand to 100% of users
  - Date: _______
  - Monitor for 1 week
  - Error rate: ______%
  - Performance impact: ______%

### Monitoring
- [ ] Error rates within acceptable range (<1%)
- [ ] Performance metrics stable
- [ ] User feedback positive
- [ ] No critical issues reported

---

## Phase 7: Stabilization & Cleanup (2-4 weeks post-rollout)

**Start Date:** _______ **Target Completion:** _______

- [ ] Monitor system for 2 weeks
- [ ] Address any edge cases
- [ ] Remove feature flags (after 4+ weeks stability)
- [ ] Archive old Service table (after 4+ weeks stability)
- [ ] Update documentation with lessons learned
- [ ] Conduct retrospective
- [ ] Archive mapping table (keep permanently)

---

## Rollback Procedures

### Rollback Triggers
- [ ] Data integrity violations detected
- [ ] Error rate >5% in critical flows
- [ ] Performance degradation >20%
- [ ] Critical business process failure

### Rollback Steps (If Needed)
1. [ ] Revert application code to previous version
2. [ ] Switch feature flags to disable new code paths
3. [ ] Restore Service table from mapping table (if dropped)
4. [ ] Validate rollback successful
5. [ ] Communicate to stakeholders
6. [ ] Schedule post-mortem
7. [ ] Plan fixes before retry

---

## Success Metrics

### Functional Metrics
- [ ] All services successfully migrated
- [ ] Mixed cart functionality working
- [ ] Order processing handles both types
- [ ] Reporting shows both product types

### Performance Metrics
- [ ] Page load time: Target < ___ ms, Actual: ___ ms
- [ ] Database query time: Target < ___ ms, Actual: ___ ms
- [ ] Error rate: Target < 1%, Actual: ___%

### Business Metrics
- [ ] No increase in cart abandonment
- [ ] No increase in order cancellations
- [ ] Customer support tickets: Target no increase, Actual: ______

---

## Key Contacts

- **Project Lead:** _________________
- **System Architect:** _________________
- **Database Administrator:** _________________
- **DevOps Lead:** _________________
- **Product Owner:** _________________
- **QA Lead:** _________________

---

## Important Notes

⚠️ **CRITICAL REMINDERS:**

1. **DO NOT start implementation without completing all Phase 0 preconditions**
2. **DO NOT make physical columns NULL without CHECK constraints**
3. **Keep ServiceToProductMapping table PERMANENTLY**
4. **Keep old Service table for at least 4 weeks after successful rollout**
5. **Use feature flags for ALL changes**
6. **Monitor error rates continuously during rollout**
7. **Have rollback runbook ready at all times**

---

## Related Documents

- [Product/Service Unification ADR](product-service-unification-adr.md)
- [Product/Service Unification ADR (HTML)](product-service-unification-adr.html)
- Migration Scripts: [TBD]
- Rollback Runbook: [TBD]
- Feature Flag Documentation: [TBD]

---

**Last Updated:** 2026-02-09  
**Next Review:** [After Phase 0 completion]
