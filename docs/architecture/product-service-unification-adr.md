# Architectural Decision Record: Product and Service Schema Unification

## Status
**Proposed** - Pending dependency audit and readiness assessment

**Date:** 2026-02-09

**Decision Makers:** [Team Lead, System Architects]

**Stakeholders:** Engineering Team, Product Management, Operations

---

## Executive Summary

This document presents an architectural decision for unifying the Product and Service data models in our ecommerce platform. Currently, these entities exist in separate tables with distinct schemas, creating operational complexity and limiting our ability to build a unified catalog experience.

**Key Decision:** Recommend unified schema approach (Plan A) over maintaining separate tables (Plan B).

**Critical Context:** This is a **platform-wide behavioral change**, not merely a schema refactor. The impact extends across cart logic, order processing, shipping, taxation, reporting, search, and integrations.

**Readiness Status:** **We are not ready to begin implementation.** A comprehensive dependency audit must be completed first to identify all systems that assume `Product == physical item`.

**Timeline:** 4–8+ weeks for implementation, depending on findings from the mandatory dependency audit.

---

## Background

### Current State

Our system maintains two separate entity tables:

**Product Table** (Physical Items)
- Primary physical product data
- Shipping-specific fields (Weight, Dimensions, PackageType)
- Inventory tracking
- Physical product lifecycle

**Service Table** (Non-Physical Offerings)
- Service-specific data
- No physical attributes
- Different pricing model
- Separate management workflows

### Problem Statement

The current separation creates several challenges:

1. **Catalog Fragmentation:** Cannot build unified product catalog
2. **Mixed Cart Complexity:** Handling physical + service in same order requires dual logic paths
3. **Code Duplication:** Similar business logic exists in parallel for both types
4. **Reporting Gaps:** Difficulty in unified revenue and analytics reporting
5. **Search Limitations:** Cannot provide unified search experience
6. **Integration Overhead:** External systems must handle two different data models

---

## Architecture Options

### Plan A: Unified Schema (Recommended)

Merge both Product and Service into a single `Product` table with a `ProductType` discriminator.

**Schema Design:**
```sql
CREATE TABLE Product (
    ProductId INT PRIMARY KEY,
    ProductType INT NOT NULL, -- 1=Physical, 2=Service
    Name NVARCHAR(500) NOT NULL,
    Description NVARCHAR(MAX),
    Price DECIMAL(18,2) NOT NULL,
    
    -- Physical-only fields with constraints
    ShippingWeight DECIMAL(10,2) NULL,
    DimensionLength DECIMAL(10,2) NULL,
    DimensionWidth DECIMAL(10,2) NULL,
    DimensionHeight DECIMAL(10,2) NULL,
    PackageType NVARCHAR(100) NULL,
    
    -- Add CHECK constraints to enforce required fields for physical products
    CONSTRAINT CK_Physical_RequiredFields CHECK (
        ProductType != 1 OR (
            ShippingWeight IS NOT NULL AND
            DimensionLength IS NOT NULL AND
            DimensionWidth IS NOT NULL AND
            DimensionHeight IS NOT NULL AND
            PackageType IS NOT NULL
        )
    ),
    
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
    ModifiedDate DATETIME NOT NULL DEFAULT GETDATE()
);

CREATE INDEX IX_Product_ProductType ON Product(ProductType);
```

**Advantages:**
- Unified catalog and search
- Simplified cart and checkout logic (single code path)
- Easier reporting and analytics
- Better long-term platform evolution
- Reduced code duplication

**Disadvantages:**
- Migration complexity for large database
- Risk of regression in existing physical product flows
- NULL columns for service products (mitigated by CHECK constraints)
- Requires comprehensive testing of all product-related code

### Plan B: Maintain Separate Tables

Keep Product and Service tables separate with improved integration layer.

**Advantages:**
- Lower immediate risk
- Isolated changes
- Faster to implement
- Clear separation of concerns

**Disadvantages:**
- Continues catalog fragmentation
- Requires dual logic paths indefinitely
- Code duplication persists
- Complex mixed-cart handling
- Long-term technical debt

---

## Decision Logic

### Choose Unified Approach (Plan A) IF:

✓ **Services are core to business strategy** (not experimental)
✓ **Unified catalog experience is required**
✓ **Mixed checkout (physical + service) is needed**
✓ **Long-term platform evolution expected**
✓ **Team capacity exists for thorough migration**
✓ **Risk can be managed through phased rollout**

### Choose Separate Approach (Plan B) IF:

✓ **Services are experimental/minimal**
✓ **Quick delivery is critical priority**
✓ **High production risk sensitivity**
✓ **Limited team capacity for complex migration**
✓ **Mixed cart is not required**
✓ **Short-term tactical solution acceptable**

### Our Recommendation: Plan A (Unified)

**Rationale:**
- Services are expected to grow significantly
- Unified catalog is strategic requirement
- Mixed checkout capability provides competitive advantage
- Platform modernization aligns with 2026+ roadmap
- Technical debt of separation compounds over time

**However:** This recommendation is **conditional** on completing the dependency audit and confirming readiness (see Readiness Assessment section).

---

## System-Wide Impact Analysis

**Critical Recognition:** This is a **platform behavioral change**, not just a schema modification.

### Systems Requiring Changes

#### 1. **Cart Logic**
- **Current Assumption:** Cart items are always physical products
- **Required Changes:**
  - Remove physical-only validation in cart service
  - Handle service items without shipping attributes
  - Update cart line item display logic
  - Modify cart totals calculation

#### 2. **Order Processing Pipeline**
- **Current Assumption:** All orders contain shippable items
- **Required Changes:**
  - Separate fulfillment flows for physical vs. service
  - Update order status transitions
  - Modify fulfillment logic to handle non-shippable items
  - Update order confirmation emails

#### 3. **Shipping & Fulfillment**
- **Current Assumption:** Every line item has weight/dimensions
- **Required Changes:**
  - Skip shipping calculations for service items
  - Filter service items from pick lists
  - Update warehouse management system integration
  - Modify shipping label generation

#### 4. **Tax Calculation**
- **Current Assumption:** Tax rules based on physical product shipping
- **Required Changes:**
  - Implement service-specific tax rules
  - Handle mixed physical/service tax calculation
  - Update tax reporting

#### 5. **Inventory Management**
- **Current Assumption:** All products have inventory levels
- **Required Changes:**
  - Exclude services from inventory tracking
  - Update stock level checks
  - Modify reorder point calculations

#### 6. **Reporting & Analytics**
- **Current Assumption:** Revenue reports assume physical product metrics
- **Required Changes:**
  - Separate revenue by product type
  - Update KPI dashboards
  - Modify sales reports to distinguish physical vs. service
  - Update margin analysis reports

#### 7. **Search & Catalog**
- **Current Assumption:** Product search includes physical attributes
- **Required Changes:**
  - Update search indexing to handle product type
  - Modify faceted navigation
  - Update product detail page templates
  - Adjust search ranking algorithms

#### 8. **Integrations**
- **Current Assumption:** External systems expect physical product data
- **Required Changes:**
  - Update ERP integration
  - Modify accounting system exports
  - Update marketplace integrations (Amazon, eBay, etc.)
  - Adjust CRM data synchronization

#### 9. **Admin Forms & Management**
- **Current Assumption:** Product admin forms require physical attributes
- **Required Changes:**
  - Conditional form rendering based on product type
  - Update validation rules
  - Modify bulk import/export
  - Update product creation workflows

#### 10. **Validations & Business Rules**
- **Current Assumption:** Business rules enforce physical product constraints
- **Required Changes:**
  - Product type-specific validation
  - Pricing rule updates
  - Promotion eligibility logic
  - Return/refund policy handling

### Estimated File Impact

Based on similar platform migrations:

- **Conservative Estimate:** 200–400 files modified
- **Realistic Estimate:** 400–600 files modified
- **Large System Estimate:** 600–800+ files modified

### Query Impact

Many existing queries likely contain assumptions:

```sql
-- Dangerous: Assumes ShippingWeight NOT NULL
SELECT ProductId, Name, ShippingWeight 
FROM Product 
WHERE ShippingWeight > 10;

-- Safer: Explicit handling
SELECT ProductId, Name, ShippingWeight 
FROM Product 
WHERE ProductType = 1 AND ShippingWeight > 10;
```

**Action Required:** Full query audit across codebase and stored procedures.

---

## Migration Safety Strategy

### Migration Approach

**Phase 1: Schema Preparation** (Week 1-2)
1. Add `ProductType` column to existing Product table (default = 1 for physical)
2. Add CHECK constraints for physical-only fields
3. Create comprehensive data validation scripts
4. Perform dry-run migration on production database copy

**Phase 2: Data Migration** (Week 2-3)
1. **Batch Migration Strategy:**
   ```sql
   -- Migrate in batches of 1000
   DECLARE @BatchSize INT = 1000;
   DECLARE @Processed INT = 0;
   
   WHILE EXISTS (SELECT 1 FROM Service WHERE MigrationStatus IS NULL)
   BEGIN
       BEGIN TRANSACTION;
       
       INSERT INTO Product (ProductType, Name, Description, Price, /* service fields */)
       SELECT 
           2 as ProductType, -- Service type
           s.Name, 
           s.Description, 
           s.Price,
           /* map service fields */
       FROM Service s
       WHERE s.MigrationStatus IS NULL
       ORDER BY s.ServiceId
       OFFSET @Processed ROWS FETCH NEXT @BatchSize ROWS ONLY;
       
       -- Update migration tracking
       UPDATE Service 
       SET MigrationStatus = 'Migrated', 
           MigratedProductId = SCOPE_IDENTITY()
       WHERE ServiceId IN (/* batch IDs */);
       
       SET @Processed = @Processed + @BatchSize;
       
       COMMIT TRANSACTION;
       
       -- Pause between batches
       WAITFOR DELAY '00:00:01';
   END
   ```

2. **Maintain Permanent Mapping Table:**
   ```sql
   CREATE TABLE ServiceToProductMapping (
       ServiceId INT NOT NULL,
       ProductId INT NOT NULL,
       MigratedDate DATETIME NOT NULL DEFAULT GETDATE(),
       CONSTRAINT PK_ServiceProductMap PRIMARY KEY (ServiceId, ProductId)
   );
   ```
   Keep this table indefinitely for auditing and rollback capability.

3. **Dual-Write Period:**
   - Update application code to write to both old and new schema
   - Duration: 2-4 weeks
   - Enables safe rollback if issues discovered

**Phase 3: Code Migration** (Week 3-6)
1. Update code in priority order:
   - Core cart logic
   - Order processing
   - Admin interfaces
   - Reporting
   - Integrations
2. Deploy behind feature flags
3. Gradual rollout by user cohort

**Phase 4: Validation & Stabilization** (Week 6-8)
1. Verify data consistency
2. Performance testing
3. User acceptance testing
4. Monitor error rates
5. Address edge cases

### Data Validation

```sql
-- Pre-migration validation
SELECT 'Product Count' as Metric, COUNT(*) as Value FROM Product
UNION ALL
SELECT 'Service Count', COUNT(*) FROM Service;

-- Post-migration validation
SELECT 'Physical Products' as Type, COUNT(*) FROM Product WHERE ProductType = 1
UNION ALL
SELECT 'Service Products', COUNT(*) FROM Product WHERE ProductType = 2;

-- Integrity checks
SELECT 'Products with NULL required physical fields' as Issue, COUNT(*)
FROM Product 
WHERE ProductType = 1 
  AND (ShippingWeight IS NULL 
       OR DimensionLength IS NULL 
       OR DimensionWidth IS NULL 
       OR DimensionHeight IS NULL);

-- Should return 0
```

### Rollback Strategy

**Rollback Triggers:**
- Data integrity violations detected
- >5% error rate in critical flows
- Performance degradation >20%
- Critical business process failure

**Rollback Procedure:**
1. Revert application code to previous version
2. Switch feature flags to disable new code paths
3. Restore Service table from mapping table if dropped
4. Data remains in Product table for investigation
5. Review and fix issues before retry

**Prerequisites for Safe Rollback:**
- Maintain ServiceToProductMapping table permanently
- Keep Service table until system stable (4+ weeks)
- Comprehensive backups at each phase
- Detailed rollback runbook

### Transaction Strategy

- **Small Tables (<10K rows):** Single transaction acceptable
- **Medium Tables (10K-1M rows):** Batch transactions (1000 rows per batch)
- **Large Tables (>1M rows):** Batch with progress tracking and pause between batches
- All migrations must be **idempotent** (safe to re-run)

### Idempotent Migration Scripts

```sql
-- Check if already migrated
IF NOT EXISTS (SELECT 1 FROM Product WHERE ProductType = 2)
BEGIN
    -- Perform migration
    -- ...
END
ELSE
BEGIN
    PRINT 'Migration already completed. Skipping.';
END
```

---

## Readiness Assessment

### Current Status: **NOT READY**

**Critical Finding:** We should **NOT begin implementation** until mandatory preconditions are met.

### Why We Are Not Ready

1. **No Dependency Audit Completed**
   - Unknown how many places assume `Product == physical`
   - Risk of missing critical code paths
   - Cannot estimate true effort

2. **Service Pricing Model Unclear**
   - How do services get priced?
   - Recurring vs. one-time?
   - Integration with existing pricing engine?

3. **Checkout Behavior Undefined**
   - Can physical + service exist in same cart?
   - Separate checkout flows or unified?
   - Payment processing differences?

4. **Migration Testing Not Validated**
   - No test migration on production copy performed
   - Data volume impact unknown
   - Migration duration uncertain

5. **Feature Flag Strategy Not Confirmed**
   - Feature flag framework exists?
   - Granularity of flags defined?
   - Rollback procedures documented?

### Required Preconditions (MANDATORY)

Before starting implementation, we **MUST complete**:

#### 1. **Full Dependency Audit**
- [ ] Identify all code files that reference Product table
- [ ] Find all queries assuming physical product attributes
- [ ] Map all stored procedures using Product
- [ ] Document all external integrations consuming Product data
- [ ] Identify reporting dependencies
- [ ] **Deliverable:** Dependency map with estimated change effort per component

#### 2. **Service Requirements Definition**
- [ ] Confirm service pricing model
- [ ] Define service fulfillment workflow
- [ ] Clarify refund/return policies for services
- [ ] Determine tax calculation approach
- [ ] **Deliverable:** Service product requirements document

#### 3. **Checkout Behavior Specification**
- [ ] Confirm mixed cart requirements (can customer buy physical + service together?)
- [ ] Define checkout flow modifications
- [ ] Specify payment processing differences
- [ ] Clarify order confirmation and fulfillment separation
- [ ] **Deliverable:** Checkout flow specification document

#### 4. **Production Database Migration Test**
- [ ] Create production database copy
- [ ] Execute migration scripts on copy
- [ ] Measure migration duration
- [ ] Validate data integrity
- [ ] Test rollback procedures
- [ ] **Deliverable:** Migration test results and performance data

#### 5. **Feature Flag Framework Confirmation**
- [ ] Verify feature flag system exists and is production-ready
- [ ] Define flag strategy for gradual rollout
- [ ] Document flag removal plan
- [ ] Test flag toggle in staging
- [ ] **Deliverable:** Feature flag implementation plan

#### 6. **Risk Mitigation Plan**
- [ ] Define success metrics
- [ ] Establish monitoring and alerting
- [ ] Create detailed rollback runbook
- [ ] Plan user communication strategy
- [ ] **Deliverable:** Risk register and mitigation playbook

### Go/No-Go Criteria

**GO Decision Requires:**
- ✅ All 6 preconditions completed
- ✅ Dependency audit shows <500 files impacted
- ✅ Migration test completes in <4 hours
- ✅ Stakeholder alignment on requirements
- ✅ Engineering team has capacity for 4-8 week project
- ✅ Feature flags tested and validated

**NO-GO if:**
- ❌ Dependency audit incomplete
- ❌ >800 files impacted (suggests 12+ week project)
- ❌ Critical requirements unclear
- ❌ No feature flag capability
- ❌ Insufficient team capacity

### Post-Audit Decision Point

After completing the dependency audit, we must **re-evaluate** whether unified approach is still correct:

- **If audit reveals manageable scope:** Proceed with Plan A
- **If audit reveals excessive complexity:** Consider Plan B or phased approach
- **If audit reveals architectural issues:** Reassess strategy entirely

---

## Timeline & Resource Estimate

### Realistic Timeline: 4–8+ Weeks

**Important:** This is a conditional estimate based on unknowns discovered during dependency audit.

### Phase Breakdown

#### **Phase 0: Readiness Assessment** (2-3 weeks) - **MUST COMPLETE FIRST**
- Week 1-2: Dependency audit
- Week 2-3: Requirements finalization
- Week 3: Production migration test

**Checkpoint:** Go/No-Go decision

#### **Phase 1: Schema & Infrastructure** (1-2 weeks)
- Database schema changes
- Feature flag setup
- Monitoring and alerting configuration
- Initial code framework changes

#### **Phase 2: Core Implementation** (2-4 weeks)
- Cart logic updates
- Order processing changes
- Shipping/fulfillment modifications
- Admin interface updates

#### **Phase 3: Supporting Systems** (1-2 weeks)
- Reporting updates
- Search re-indexing
- Integration updates
- Documentation

#### **Phase 4: Testing & Validation** (1-2 weeks)
- Integration testing
- Performance testing
- User acceptance testing
- Security review

#### **Phase 5: Gradual Rollout** (1-2 weeks)
- Deploy to 5% users
- Monitor metrics
- Expand to 25% users
- Expand to 100% users

### Total Timeline: 7-13 Weeks

- **Minimum:** 7 weeks (with perfect execution, no unknowns)
- **Realistic:** 9-11 weeks (accounting for typical issues)
- **Conservative:** 11-13 weeks (with unknown complexity from audit)

### Why Previous 3-4 Week Estimate Was Unrealistic

For a mature ecommerce system with:
- Large database
- Many forms
- Cart and order processing
- Reports
- Integrations

A unified ProductType refactor typically impacts:
- **200-800 files** across codebase
- **50-200 database queries/procedures**
- **10-50 integration points**
- **20-100 test files**

**3-4 weeks would only be feasible for:**
- Small system (<50k products)
- Limited integrations
- Simple order flow
- Small engineering team doing targeted changes

**Our system requires:**
- Comprehensive dependency audit (2-3 weeks)
- Staged migration for large database
- Extensive testing of critical flows
- Integration updates
- Gradual rollout with monitoring

### Resource Requirements

- **Backend Engineers:** 2-3 FTE
- **Frontend Engineers:** 1-2 FTE (admin UI changes)
- **QA Engineers:** 1-2 FTE
- **Database Administrator:** 0.5 FTE
- **DevOps Engineer:** 0.5 FTE (monitoring, deployment)
- **Product Owner:** 0.25 FTE (requirements, UAT)

---

## Risk Analysis

### High Risks

| Risk | Impact | Probability | Mitigation |
|------|--------|-------------|------------|
| **Regression in existing checkout** | Critical | Medium | Comprehensive testing, feature flags, gradual rollout |
| **Data migration failure** | Critical | Low | Test on production copy, idempotent scripts, rollback plan |
| **Performance degradation** | High | Medium | Performance testing, query optimization, index strategy |
| **Integration breakage** | High | Medium | Integration testing, versioned APIs, backward compatibility |
| **Incomplete dependency discovery** | High | High | Thorough audit, code analysis tools, staged rollout |

### Medium Risks

| Risk | Impact | Probability | Mitigation |
|------|--------|-------------|------------|
| **Timeline overrun** | Medium | High | Buffer in estimates, prioritize MVP, defer nice-to-have |
| **Team capacity constraints** | Medium | Medium | Identify critical path, consider external help |
| **Requirement changes mid-project** | Medium | Medium | Freeze requirements after readiness phase |
| **Test environment limitations** | Medium | Low | Ensure test environment parity with production |

### Low Risks

| Risk | Impact | Probability | Mitigation |
|------|--------|-------------|------------|
| **Training needs for team** | Low | Medium | Documentation, knowledge sharing sessions |
| **User confusion** | Low | Low | Clear communication, updated help documentation |

---

## Anticipated Challenge Questions

Prepare answers for these expected questions from senior architects:

### 1. **"How many places assume product = physical?"**
**Answer after audit:** [To be determined in dependency audit]
**Current assumption:** Significant - likely 100+ code locations based on system age

### 2. **"Can cart handle service with no shipping?"**
**Answer:** Cart currently assumes all items are physical. Changes required in:
- Cart validation logic
- Shipping calculation service
- Cart totals calculation
- Cart display components

### 3. **"Can order pipeline handle non-shippable items?"**
**Answer:** Current order pipeline assumes all items require fulfillment. Changes required:
- Separate fulfillment workflow for services
- Order status state machine updates
- Fulfillment service logic
- Warehouse system integration filtering

### 4. **"How will reporting separate revenue types?"**
**Answer:** Add `ProductType` dimension to all revenue reports:
- Sales by product type
- Revenue mix analysis
- Margin analysis by type
- Inventory turnover (physical only)

### 5. **"Rollback if migration fails?"**
**Answer:** 
- Phase 1-2: Full rollback possible (mapping table maintained)
- Phase 3+: Forward-fix preferred (code deployed, but flags can revert behavior)
- Service table kept for 4+ weeks post-migration
- Detailed rollback runbook documented

### 6. **"Mixed cart needed or separate?"**
**Answer:** [Requires product team input] 
- **If mixed cart required:** Unified approach strongly preferred
- **If separate checkout acceptable:** Could simplify initial implementation

### 7. **"Why unify now instead of phase later?"**
**Answer:** 
- Service growth is strategic priority for 2026
- Catalog unification provides competitive advantage
- Technical debt of separate systems compounds
- Unified approach enables future capabilities (subscriptions, bundles)
- **However:** Decision pending dependency audit results

---

## Decision Recommendation

### Recommended Path: Plan A (Unified Schema) - **CONDITIONAL**

**Conditions:**
1. ✅ Dependency audit completed
2. ✅ All readiness preconditions met
3. ✅ Audit reveals manageable scope (<600 files)
4. ✅ Stakeholder alignment achieved
5. ✅ Team capacity confirmed

**If conditions NOT met:** Re-evaluate or choose Plan B.

### Rationale for Recommendation

1. **Strategic Alignment:** Services are core to 2026+ business strategy
2. **Competitive Advantage:** Unified catalog differentiates our platform
3. **Long-term Value:** Reduces technical debt, enables future capabilities
4. **User Experience:** Better search, browsing, and purchase experience
5. **Operational Efficiency:** Single code path reduces maintenance burden

### Implementation Approach

1. **Phase 0:** Complete all readiness preconditions (2-3 weeks)
2. **Go/No-Go Decision:** Based on audit findings
3. **Phased Implementation:** 4-8 weeks with feature flags
4. **Gradual Rollout:** Monitor metrics at each stage
5. **Stabilization:** 2-4 weeks post-full-rollout

### Next Steps

1. **Immediate:** Begin dependency audit (highest priority)
2. **Week 1-2:** Complete audit and readiness assessment
3. **Week 3:** Finalize requirements and test migration
4. **Week 3 End:** Go/No-Go decision point
5. **Week 4+:** Begin implementation if GO

---

## Appendix: Additional Considerations

### Database Constraint Strategy

**DO NOT make physical columns NULL globally without constraints.**

This is risky because:
- Existing queries may assume `ShippingWeight NOT NULL`
- Validation logic may break
- Reports may fail

**Instead, use CHECK constraints:**

```sql
ALTER TABLE Product
ADD CONSTRAINT CK_Physical_ShippingWeight 
CHECK (ProductType != 1 OR ShippingWeight IS NOT NULL);

ALTER TABLE Product
ADD CONSTRAINT CK_Physical_Dimensions
CHECK (ProductType != 1 OR (
    DimensionLength IS NOT NULL AND
    DimensionWidth IS NOT NULL AND
    DimensionHeight IS NOT NULL
));
```

This enforces:
- Physical products MUST have shipping data
- Services can have NULL shipping data
- Database maintains referential integrity

### Performance Considerations

- Add index on `ProductType` for filtered queries
- Consider partitioning if Product table >10M rows
- Monitor query plan changes post-migration
- Update statistics after data migration

### Testing Strategy

1. **Unit Tests:** All modified business logic
2. **Integration Tests:** End-to-end cart and order flows
3. **Performance Tests:** Load testing with realistic data volumes
4. **Regression Tests:** Existing critical user journeys
5. **User Acceptance Testing:** Real users test new flows

---

## Document Revision History

| Version | Date | Author | Changes |
|---------|------|--------|---------|
| 1.0 | 2026-02-09 | Architecture Team | Initial senior-level submission |

---

## Approval Signatures

**Prepared by:** Architecture Team

**Reviewed by:** 
- [ ] Engineering Lead
- [ ] System Architect
- [ ] Product Owner
- [ ] DevOps Lead

**Approved by:**
- [ ] CTO / VP Engineering

---

**Classification:** Internal Use Only  
**Next Review Date:** [After dependency audit completion]
